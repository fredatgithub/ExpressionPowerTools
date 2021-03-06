﻿// Copyright (c) Jeremy Likness. All rights reserved.
// Licensed under the MIT License. See LICENSE in the repository root for license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionPowerTools.Core.Comparisons;
using ExpressionPowerTools.Utilities.DocumentGenerator.Hierarchy;
using ExpressionPowerTools.Utilities.DocumentGenerator.Markdown;

namespace ExpressionPowerTools.Utilities.DocumentGenerator.Parsers
{
    /// <summary>
    /// Class to transform documentation objects into markdown.
    /// </summary>
    public class DocsToMarkdownParser
    {
        /// <summary>
        /// The <see cref="MarkdownWriter"/> to use.
        /// </summary>
        private readonly MarkdownWriter writer = new MarkdownWriter();

        /// <summary>
        /// Parses a <see cref="DocAssembly"/> to return a <see cref="DocFile"/>.
        /// </summary>
        /// <param name="assembly">The <see cref="DocAssembly"/> to parse.</param>
        /// <returns>The transformed <see cref="DocFile"/>.</returns>
        public DocFile Parse(DocAssembly assembly)
        {
            var result = new DocFile(assembly.FileName);

            result.AddThenBlankLine(writer.WriteHeading1($"{assembly.Name} API Reference"));
            result.AddThenBlankLine(ParserUtils.ProcessBreadcrumb(this));

            var list = new MarkdownList();

            foreach (var customDoc in assembly.CustomDocs)
            {
                if (customDoc is DocSerialization ser)
                {
                    list.AddItem(writer.WriteLink($"{ser.Name.NameOnly()} Reference", ser.FileName));
                    result.Files.Add(ProcessSerialization(ser, assembly));
                }
            }

            foreach (var ns in assembly.Namespaces.OrderBy(ns => ns.Name))
            {
                list.AddItem(writer.WriteLink(ns.Name, ns.FileName));
                result.Files.Add(ProcessNamespace(ns));
            }

            writer.AddRange(result.Markdown, list.CloseList());
            return result;
        }

        private DocFile ProcessSerialization(DocSerialization ser, DocAssembly assembly)
        {
            var result = new DocFile(ser.FileName);
            result.AddThenBlankLine(writer.WriteHeading1($"{ser.Name} Reference"));
            result.AddThenBlankLine(ParserUtils.ProcessBreadcrumb(assembly));
            var table = new MarkdownTable("ExpressionType", "Serializer");
            foreach (ExpressionType expressionType in Enum.GetValues(typeof(ExpressionType)))
            {
                var name = expressionType.ToString();
                if (ser.Serializers.ContainsKey(expressionType))
                {
                    var entry = ser.Serializers[expressionType];
                    table.AddRow(name, writer.WriteLink(entry.Name.NameOnly(), entry.FileName));
                }
                else
                {
                    table.AddRow(name, "_Not Supported_");
                }
            }

            writer.AddRange(result.Markdown, table.CloseTable());
            return result;
        }

        /// <summary>
        /// Parses a <see cref="DocNamespace"/> into a <see cref="DocFile"/>.
        /// </summary>
        /// <param name="ns">The <see cref="DocNamespace"/> to parse.</param>
        /// <returns>The transformed <see cref="DocFile"/>.</returns>
        private DocFile ProcessNamespace(DocNamespace ns)
        {
            var result = new DocFile(ns.FileName);

            result.AddThenBlankLine(writer.WriteHeading1($"{ns.Name} Namespace"));
            result.AddThenBlankLine(ParserUtils.ProcessBreadcrumb(ns));

            if (ns.Types.Any(t => t.IsClass))
            {
                result.AddThenBlankLine(writer.WriteHeading2("Classes"));
                var table = new MarkdownTable("Class", "Description");
                foreach (var c in ns.Types.Where(t => t.IsClass).OrderBy(t => t.TypeRef.FriendlyName))
                {
                    table.AddRow(writer.WriteLink(c.TypeRef.FriendlyName, c.FileName), c.Description);
                    result.Files.Add(ProcessType(c));
                }

                writer.AddRange(result.Markdown, table.CloseTable());
                result.AddBlankLine();
            }

            if (ns.Types.Any(t => t.IsInterface))
            {
                result.AddThenBlankLine(writer.WriteHeading2("Interfaces"));
                var table = new MarkdownTable("Interface", "Description");
                foreach (var i in ns.Types.Where(t => t.IsInterface).OrderBy(t => t.TypeRef.FriendlyName))
                {
                    table.AddRow(writer.WriteLink(i.TypeRef.FriendlyName, i.FileName), i.Description);
                    result.Files.Add(ProcessType(i));
                }

                writer.AddRange(result.Markdown, table.CloseTable());
                result.AddBlankLine();
            }

            if (ns.Types.Any(t => t.IsEnum))
            {
                result.AddThenBlankLine(writer.WriteHeading2("Enumerations"));
                var table = new MarkdownTable("Enumeration", "Description");
                foreach (var i in ns.Types.Where(t => t.IsEnum).OrderBy(t => t.TypeRef.FriendlyName))
                {
                    table.AddRow(writer.WriteLink(i.TypeRef.FriendlyName.NameOnly(), i.FileName), i.Description);
                    result.Files.Add(ProcessType(i));
                }

                writer.AddRange(result.Markdown, table.CloseTable());
                result.AddBlankLine();
            }

            return result;
        }

        /// <summary>
        /// Parses a <see cref="DocExportedType"/> to a <see cref="DocFile"/>.
        /// </summary>
        /// <param name="t">The <see cref="DocExportedType"/> to parse.</param>
        /// <returns>The transformed <see cref="DocFile"/>.</returns>
        private DocFile ProcessType(DocExportedType t)
        {
            var result = new DocFile(t.FileName);

            var classification = t.IsInterface ? "Interface" : (t.IsEnum ? "Enum" : "Class");
            result.AddThenBlankLine(writer.WriteHeading1($"{MarkdownWriter.Normalize(t.TypeRef.FriendlyName)} {classification}"));

            result.AddThenBlankLine(ParserUtils.ProcessBreadcrumb(t));
            result.AddThenBlankLine(t.Description);
            ExtractCode(t.Code, result);

            ExtractTypeParameters(t.TypeParameters, result);

            if (t.Inheritance.Any())
            {
                result.AddThenBlankLine(ParserUtils.ParseInheritance(t.Inheritance));
            }

            if (t.ImplementedInterfaces.Any())
            {
                result.AddThenBlankLine(ParserUtils
                    .ParseImplementedInterfaces(t.ImplementedInterfaces));
            }

            if (t.DerivedTypes.Any())
            {
                result.AddThenBlankLine(ParserUtils
                    .ParseDerivedTypes(t.DerivedTypes));
            }

            ExtractExamples(t.Example, result);

            ExtractRemarks(t.Remarks, result);

            ExtractCtors(t.Constructor, result);

            ExtractProperties(t.Properties, result);

            ExtractMethods(t, result);

            return result;
        }

        /// <summary>
        /// Extracts the constructors into a table and files.
        /// </summary>
        /// <param name="constructor">The <see cref="DocConstructor"/>.</param>
        /// <param name="result">The <see cref="DocFile"/> to parse to.</param>
        private void ExtractCtors(DocConstructor constructor, DocFile result)
        {
            if (constructor == null || !constructor.Overloads.Any())
            {
                return;
            }

            var ctorFile = new DocFile(constructor.FileName);

            result.Files.Add(ctorFile);
            result.AddThenBlankLine(writer.WriteHeading2($"Constructors"));

            ctorFile.AddThenBlankLine(writer.WriteHeading1(
                $"{MarkdownWriter.Normalize(constructor.ConstructorType.TypeRef.FriendlyName.NameOnly())} Constructors"));
            ctorFile.AddThenBlankLine(ParserUtils.ProcessBreadcrumb(constructor));
            ctorFile.AddThenBlankLine(constructor.Overloads[0].Description);
            ctorFile.AddThenBlankLine(writer.WriteHeading2("Overloads"));

            var table = new MarkdownTable("Ctor", "Description");
            var tableCtor = new MarkdownTable("Ctor", "Description");

            static string GetName(DocOverload overload)
            {
                var name = overload.Name.Split(".")[^1];

                if (overload.IsStatic)
                {
                    name = $"static {name}";
                }

                return name;
            }

            foreach (var overload in constructor.Overloads)
            {
                var name = GetName(overload);

                table.AddRow(
                    writer.WriteRelativeLink(name, constructor.FileName),
                    overload.Description);

                tableCtor.AddRow(
                    writer.WriteRelativeLink(
                        name),
                    overload.Description);
            }

            writer.AddRange(result.Markdown, table.CloseTable());
            writer.AddRange(ctorFile.Markdown, tableCtor.CloseTable());

            foreach (var overload in constructor.Overloads)
            {
                var name = GetName(overload);
                ctorFile.AddBlankLine();
                ctorFile.AddThenBlankLine(writer.WriteHeading2(name));
                ctorFile.AddThenBlankLine(overload.Description);
                ExtractTypeParameters(overload.TypeParameters, ctorFile);
                ExtractCode(overload.Code, ctorFile);
                ExtractParameters(overload.Parameters, ctorFile);
                ExtractExceptions(overload.Exceptions, ctorFile);
                ExtractExamples(overload.Example, ctorFile);
                ExtractRemarks(overload.Remarks, ctorFile);
            }
        }

        /// <summary>
        /// Extracts the methods into a table and files.
        /// </summary>
        /// <param name="type">The <see cref="DocExportedType"/>.</param>
        /// <param name="result">The <see cref="DocFile"/> to parse to.</param>
        private void ExtractMethods(DocExportedType type, DocFile result)
        {
            if (!type.Methods.Any())
            {
                return;
            }

            result.AddThenBlankLine(writer.WriteHeading2($"Methods"));

            var methodTable = new MarkdownTable("Method", "Description");

            foreach (var method in type.Methods.OrderBy(m => m.MethodOverloads[0].TerseName))
            {
                var methodFile = new DocFile(method.FileName);

                result.Files.Add(methodFile);

                methodTable.AddRow(
                    writer.WriteLink(
                        $"{method.MethodReturnType.FriendlyName} {method.MethodOverloads[0].TerseName}",
                        method.FileName),
                    method.MethodOverloads[0].Description);

                var parent = MarkdownWriter.Normalize(method.MethodType.TypeRef.FriendlyName);

                methodFile.AddThenBlankLine(writer.WriteHeading1(
                    $"{parent}.{MarkdownWriter.Normalize(method.Name)} Method"));
                methodFile.AddThenBlankLine(ParserUtils.ProcessBreadcrumb(method));
                methodFile.AddThenBlankLine(method.MethodOverloads[0].Description);
                methodFile.AddThenBlankLine(writer.WriteHeading2("Overloads"));

                var overloadTable = new MarkdownTable("Overload", "Description");
                var orderedOverloads = method.MethodOverloads.OrderBy(o => o.TerseName);

                foreach (var overload in orderedOverloads)
                {
                    overloadTable.AddRow(
                        writer.WriteRelativeLink(overload.TerseName),
                        overload.Description);
                }

                writer.AddRange(methodFile.Markdown, overloadTable.CloseTable());

                foreach (var overload in orderedOverloads)
                {
                    methodFile.AddThenBlankLine(writer.WriteHeading2(overload.TerseName));
                    methodFile.AddThenBlankLine(overload.Description);
                    ExtractCode(overload.Code, methodFile);
                    ExtractTypeParameters(overload.TypeParameters, methodFile);
                    methodFile.AddThenBlankLine(writer.WriteHeading3("Return Type"));
                    var returns = writer.WriteLink(overload.Method.MethodReturnType);
                    if (!string.IsNullOrWhiteSpace(overload.Returns))
                    {
                        returns += $" - {overload.Returns}";
                    }

                    methodFile.AddThenBlankLine(returns);
                    ExtractParameters(overload.Parameters, methodFile);
                    ExtractExceptions(overload.Exceptions, methodFile);
                    ExtractExamples(overload.Example, methodFile);
                    ExtractRemarks(overload.Remarks, methodFile);
                }
            }

            writer.AddRange(result.Markdown, methodTable.CloseTable());
        }

        /// <summary>
        /// Extracts the table of exceptions.
        /// </summary>
        /// <param name="exceptions">The list of exceptions.</param>
        /// <param name="docFile">The file to write to.</param>
        private void ExtractExceptions(IList<(string exception, string description)> exceptions, DocFile docFile)
        {
            if (exceptions.Any())
            {
                docFile.AddThenBlankLine(writer.WriteHeading3("Exceptions"));
                var table = new MarkdownTable("Exception", "Description");
                foreach (var exception in exceptions)
                {
                    table.AddRow(exception.exception, exception.description);
                }

                writer.AddRange(docFile.Markdown, table.CloseTable());
            }

            docFile.AddBlankLine();
        }

        /// <summary>
        /// Extra the parameters from the constructor.
        /// </summary>
        /// <param name="parameters">The list of <see cref="DocParameter"/>.</param>
        /// <param name="docFile">The <see cref="DocFile"/> target.</param>
        private void ExtractParameters(IList<DocParameter> parameters, DocFile docFile)
        {
            if (parameters.Any())
            {
                docFile.AddThenBlankLine(writer.WriteHeading3("Parameters"));
                var table = new MarkdownTable("Parameter", "Type", "Description");
                foreach (var parameter in parameters)
                {
                    table.AddRow(
                        $"`{parameter.Name}`",
                        writer.WriteLink(parameter.ParameterType.TypeRef),
                        parameter.Description);
                }

                writer.AddRange(docFile.Markdown, table.CloseTable());
                docFile.AddBlankLine();
            }
        }

        /// <summary>
        /// Extract the properties from parent type.
        /// </summary>
        /// <param name="properties">The list of <see cref="DocProperty"/>.</param>
        /// <param name="docFile">The <see cref="DocFile"/> target.</param>
        /// <param name="customHeading">Optional custom heading.</param>
        private void ExtractProperties(
            IList<DocProperty> properties,
            DocFile docFile,
            string customHeading = null)
        {
            if (properties.Any())
            {
                docFile.AddThenBlankLine(writer.WriteHeading2("Properties"));
                var table = customHeading == null ?
                    new MarkdownTable("Property", "Type", "Description") :
                    new MarkdownTable("Property", "Type", "Description", customHeading);
                foreach (var property in properties.OrderBy(p => p.Name))
                {
                    string typeLink;

                    typeLink = writer.WriteLink(property.TypeRef);

                    var name = property.IsIndexer ?
                        $"Index {property.IndexName}" :
                        property.Name.NameOnly();

                    if (customHeading == null)
                    {
                        table.AddRow(
                            writer.WriteLink(
                                $"`{name}`",
                                property.FileName),
                            typeLink,
                            property.Description);
                    }
                    else
                    {
                        table.AddRow(
                        writer.WriteLink(
                            $"`{name}`",
                            property.FileName),
                        typeLink,
                        property.Description,
                        property.CustomInfo);
                    }

                    var propertyDoc = new DocFile(property.FileName);
                    propertyDoc.AddThenBlankLine(writer.WriteHeading1(
                        $"{property.ParentType.TypeRef.FriendlyName}.{name} Property"));
                    propertyDoc.AddThenBlankLine(ParserUtils.ProcessBreadcrumb(property));
                    if (!string.IsNullOrWhiteSpace(property.Description))
                    {
                        propertyDoc.AddThenBlankLine(property.Description);
                    }

                    ExtractCode(property.Code, propertyDoc);
                    ExtractTypeParameters(property.TypeParameters, propertyDoc);
                    ExtractExamples(property.Example, propertyDoc);
                    ExtractRemarks(property.Remarks, propertyDoc);
                    propertyDoc.AddThenBlankLine(writer.WriteHeading3("Property Value"));
                    propertyDoc.AddThenBlankLine(typeLink);
                    if (property.TypeParameter != null && property.TypeParameter.TypeConstraints.Any())
                    {
                        propertyDoc.AddThenBlankLine("**Type Constraints**");
                        var list = new MarkdownList();
                        foreach (var constraint in property.TypeParameter.TypeConstraints)
                        {
                            list.AddItem(writer.WriteLink(constraint));
                        }

                        writer.AddRange(propertyDoc.Markdown, list.CloseList());
                    }

                    docFile.Files.Add(propertyDoc);
                }

                writer.AddRange(docFile.Markdown, table.CloseTable());
                docFile.AddBlankLine();
            }
        }

        /// <summary>
        /// Extracts type parameters into a <see cref="DocFile"/>.
        /// </summary>
        /// <param name="typeParameters">The type parameters to parse.</param>
        /// <param name="result">The <see cref="DocFile"/> to write to.</param>
        private void ExtractTypeParameters(IList<DocTypeParameter> typeParameters, DocFile result)
        {
            if (typeParameters.Any())
            {
                result.AddThenBlankLine(writer.WriteHeading3("Type Parameters"));
                var table = new MarkdownTable("Parameter Name", "Constraints", "Description");
                foreach (var tParam in typeParameters)
                {
                    string constraints = "None.";
                    if (tParam.TypeConstraints.Any())
                    {
                        constraints =
                            string.Join(
                                " ",
                                tParam.TypeConstraints.Select(c => writer.WriteLink(c)).ToArray());
                    }

                    table.AddRow(
                        $"`{tParam.Variance}{tParam.Name}`",
                        constraints,
                        tParam.Description);
                }

                writer.AddRange(result.Markdown, table.CloseTable());
                result.AddBlankLine();
            }
        }

        /// <summary>
        /// Extracts examples.
        /// </summary>
        /// <param name="example">The example to parse.</param>
        /// <param name="result">The <see cref="DocFile"/> to parse to.</param>
        private void ExtractExamples(string example, DocFile result)
        {
            if (!string.IsNullOrWhiteSpace(example))
            {
                result.AddThenBlankLine(writer.WriteHeading2("Examples"));
                result.AddThenBlankLine(example);
            }
        }

        /// <summary>
        /// Extracts remarks.
        /// </summary>
        /// <param name="remarks">The remarks to parse.</param>
        /// <param name="result">The <see cref="DocFile"/> to parse to.</param>
        private void ExtractRemarks(string remarks, DocFile result)
        {
            if (!string.IsNullOrWhiteSpace(remarks))
            {
                result.AddThenBlankLine(writer.WriteHeading2("Remarks"));
                result.AddThenBlankLine(remarks);
            }
        }

        /// <summary>
        /// Extracts code.
        /// </summary>
        /// <param name="code">The code to parse.</param>
        /// <param name="result">The <see cref="DocFile"/> to parse to.</param>
        private void ExtractCode(string code, DocFile result)
        {
            if (!string.IsNullOrWhiteSpace(code))
            {
                writer.AddRange(result.Markdown, writer.WriteCode(code));
                result.AddBlankLine();
            }
        }
    }
}
