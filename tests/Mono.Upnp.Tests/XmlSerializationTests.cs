// 
// XmlSerializationTests.cs
//  
// Author:
//       Scott Peterson <lunchtimemama@gmail.com>
// 
// Copyright (c) 2009 Scott Peterson
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;

namespace Mono.Upnp.Xml.Tests
{
    [TestFixture]
    public class XmlSerializationTests
    {
        readonly XmlSerializer serializer = new XmlSerializer ();
        
        class EmptyTestClass
        {
            public override string ToString () { return "Blarg!"; }
        }
        
        [Test]
        public void EmptyTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><EmptyTestClass>Blarg!</EmptyTestClass>",
                serializer.GetString (new EmptyTestClass ())
            );
        }
        
        [Test]
        public void GetBytesTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><EmptyTestClass>Blarg!</EmptyTestClass>",
                Encoding.UTF8.GetString (serializer.GetBytes (new EmptyTestClass ()))
            );
        }
        
        [XmlType ("test")]
        class TypeTestClass
        {
            public override string ToString () { return "Blarg!"; }
        }
        
        [Test]
        public void TypeTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><test>Blarg!</test>",
                serializer.GetString (new TypeTestClass ())
            );
        }
        
        [XmlType ("test", "urn:mono-upnp:tests")]
        class TypeNamespaceTestClass
        {
            public override string ToString () { return "Blarg!"; }
        }
        
        [Test]
        public void TypeNamespaceTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><test xmlns=""urn:mono-upnp:tests"">Blarg!</test>",
                serializer.GetString (new TypeNamespaceTestClass ())
            );
        }
        
        [XmlType ("test", "urn:mono-upnp:tests", "test")]
        class TypePrefixTestClass
        {
            public override string ToString () { return "Blarg!"; }
        }
        
        [Test]
        public void TypePrefixTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><test:test xmlns:test=""urn:mono-upnp:tests"">Blarg!</test:test>",
                serializer.GetString (new TypePrefixTestClass ())
            );
        }
        
        class ElementTestClass
        {
            [XmlElement] public string Foo { get; set; }
        }
        
        [Test]
        public void ElementTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ElementTestClass><Foo>bar</Foo></ElementTestClass>",
                serializer.GetString (new ElementTestClass { Foo = "bar" })
            );
        }
        
        [Test]
        public void ElementIncludeIfNullTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ElementTestClass><Foo /></ElementTestClass>",
                serializer.GetString (new ElementTestClass ())
            );
        }
        
        class ElementOmitIfNullTestClass
        {
            [XmlElement (OmitIfNull = true)] public string Foo { get; set; }
        }
        
        [Test]
        public void ElementOmitIfNullTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ElementOmitIfNullTestClass />",
                serializer.GetString (new ElementOmitIfNullTestClass ())
            );
        }
        
        class ElementNameTestClass
        {
            [XmlElement ("foo")] public string Foo { get; set; }
        }
        
        [Test]
        public void ElementNameTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ElementNameTestClass><foo>bar</foo></ElementNameTestClass>",
                serializer.GetString (new ElementNameTestClass { Foo = "bar" })
            );
        }
        
        class ElementNamespaceTestClass
        {
            [XmlElement ("foo", "urn:mono-upnp:tests")] public string Foo { get; set; }
        }
        
        [Test]
        public void ElementNamespaceTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ElementNamespaceTestClass><foo xmlns=""urn:mono-upnp:tests"">bar</foo></ElementNamespaceTestClass>",
                serializer.GetString (new ElementNamespaceTestClass { Foo = "bar" })
            );
        }
        
        class ElementPrefixTestClass
        {
            [XmlElement ("foo", "urn:mono-upnp:tests", "test")] public string Foo { get; set; }
        }
        
        [Test]
        public void ElementPrefixTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ElementPrefixTestClass><test:foo xmlns:test=""urn:mono-upnp:tests"">bar</test:foo></ElementPrefixTestClass>",
                serializer.GetString (new ElementPrefixTestClass { Foo = "bar" })
            );
        }
        
        class AttributeTestClass
        {
            [XmlAttribute] public string Foo { get; set; }
        }
        
        [Test]
        public void AttributeTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><AttributeTestClass Foo=""bar"" />",
                serializer.GetString (new AttributeTestClass { Foo = "bar" })
            );
        }
        
        [Test]
        public void AttributeIncludeIfNullTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><AttributeTestClass Foo="""" />",
                serializer.GetString (new AttributeTestClass ())
            );
        }
        
        class AttributeOmitIfNullTestClass
        {
            [XmlAttribute (OmitIfNull = true)] public string Foo { get; set; }
        }
        
        [Test]
        public void AttributeOmitIfNullTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><AttributeOmitIfNullTestClass />",
                serializer.GetString (new AttributeOmitIfNullTestClass ())
            );
        }
        
        class AttributeNameTestClass
        {
            [XmlAttribute ("foo")] public string Foo { get; set; }
        }
        
        [Test]
        public void AttributeNameTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><AttributeNameTestClass foo=""bar"" />",
                serializer.GetString (new AttributeNameTestClass { Foo = "bar" })
            );
        }
        
        class AttributePrefixTestClass
        {
            [XmlAttribute ("foo", "urn:mono-upnp:tests", "test")] public string Foo { get; set; }
        }
        
        [Test]
        public void AttributePrefixTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><AttributePrefixTestClass test:foo=""bar"" xmlns:test=""urn:mono-upnp:tests"" />",
                serializer.GetString (new AttributePrefixTestClass { Foo = "bar" })
            );
        }
        
        class FlagTestClass
        {
            [XmlFlag] public bool Foo { get; set; }
        }
        
        [Test]
        public void FlagPresentTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><FlagTestClass><Foo /></FlagTestClass>",
                serializer.GetString (new FlagTestClass { Foo = true })
            );
        }
        
        [Test]
        public void FlagAbsentTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><FlagTestClass />",
                serializer.GetString (new FlagTestClass { Foo = false })
            );
        }
        
        class FlagNameTestClass
        {
            [XmlFlag ("foo")] public bool Foo { get; set; }
        }
        
        [Test]
        public void FlagNameTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><FlagNameTestClass><foo /></FlagNameTestClass>",
                serializer.GetString (new FlagNameTestClass { Foo = true })
            );
        }
        
        class FlagNamespaceTestClass
        {
            [XmlFlag ("foo", "urn:mono-upnp:tests")] public bool Foo { get; set; }
        }
        
        [Test]
        public void FlagNamespaceTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><FlagNamespaceTestClass><foo xmlns=""urn:mono-upnp:tests"" /></FlagNamespaceTestClass>",
                serializer.GetString (new FlagNamespaceTestClass { Foo = true })
            );
        }
        
        class FlagPrefixTestClass
        {
            [XmlFlag ("foo", "urn:mono-upnp:tests", "test")] public bool Foo { get; set; }
        }
        
        [Test]
        public void FlagPrefixTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><FlagPrefixTestClass><test:foo xmlns:test=""urn:mono-upnp:tests"" /></FlagPrefixTestClass>",
                serializer.GetString (new FlagPrefixTestClass { Foo = true })
            );
        }
        
        class Item
        {
            [XmlAttribute] public string Name { get; set; }
        }
        
        class ArrayTestClass
        {
            [XmlArray] public Item[] Items { get; set; }
        }
        
        [Test]
        public void ArrayTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayTestClass><Items><Item Name=""Foo"" /><Item Name=""Bar"" /></Items></ArrayTestClass>",
                serializer.GetString (new ArrayTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class ArrayNameTestClass
        {
            [XmlArray ("items")] public Item[] Items { get; set; }
        }
        
        [Test]
        public void ArrayNameTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayNameTestClass><items><Item Name=""Foo"" /><Item Name=""Bar"" /></items></ArrayNameTestClass>",
                serializer.GetString (new ArrayNameTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class ArrayNamespaceTestClass
        {
            [XmlArray ("items", "udn:mono-upnp:tests")] public Item[] Items { get; set; }
        }
        
        [Test]
        public void ArrayNamespaceTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayNamespaceTestClass><items xmlns=""udn:mono-upnp:tests""><Item Name=""Foo"" /><Item Name=""Bar"" /></items></ArrayNamespaceTestClass>",
                serializer.GetString (new ArrayNamespaceTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class ArrayPrefixTestClass
        {
            [XmlArray ("items", "udn:mono-upnp:tests", "test")] public Item[] Items { get; set; }
        }
        
        [Test]
        public void ArrayPrefixTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayPrefixTestClass><test:items xmlns:test=""udn:mono-upnp:tests""><Item Name=""Foo"" /><Item Name=""Bar"" /></test:items></ArrayPrefixTestClass>",
                serializer.GetString (new ArrayPrefixTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class ArrayItemTestClass
        {
            [XmlArray, XmlArrayItem ("item")] public Item[] Items { get; set; }
        }
        
        [Test]
        public void ArrayItemTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayItemTestClass><Items><item Name=""Foo"" /><item Name=""Bar"" /></Items></ArrayItemTestClass>",
                serializer.GetString (new ArrayItemTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class ArrayItemNamespaceTestClass
        {
            [XmlArray, XmlArrayItem ("item", "udn:mono-upnp:tests")] public Item[] Items { get; set; }
        }
        
        [Test]
        public void ArrayItemNamespaceTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayItemNamespaceTestClass><Items><item Name=""Foo"" xmlns=""udn:mono-upnp:tests"" /><item Name=""Bar"" xmlns=""udn:mono-upnp:tests"" /></Items></ArrayItemNamespaceTestClass>",
                serializer.GetString (new ArrayItemNamespaceTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class ArrayItemPrefixTestClass
        {
            [XmlArray, XmlArrayItem ("item", "udn:mono-upnp:tests", "test")] public Item[] Items { get; set; }
        }
        
        [Test]
        public void ArrayItemPrefixTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayItemPrefixTestClass><Items><test:item Name=""Foo"" xmlns:test=""udn:mono-upnp:tests"" /><test:item Name=""Bar"" xmlns:test=""udn:mono-upnp:tests"" /></Items></ArrayItemPrefixTestClass>",
                serializer.GetString (new ArrayItemPrefixTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class ArrayIEnumerableTestClass
        {
            [XmlArray] public IEnumerable<Item> Items { get; set; }
        }
        
        [Test]
        public void ArrayIEnumerableTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ArrayIEnumerableTestClass><Items><Item Name=""Foo"" /><Item Name=""Bar"" /></Items></ArrayIEnumerableTestClass>",
                serializer.GetString (new ArrayIEnumerableTestClass { Items = new Item[] { new Item { Name = "Foo" }, new Item { Name = "Bar"} } })
            );
        }
        
        class RecursiveTestClass
        {
            [XmlElement (OmitIfNull = true)] public RecursiveTestClass Child { get; set; }
        }
        
        [Test]
        public void RecursiveTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><RecursiveTestClass><Child /></RecursiveTestClass>",
                serializer.GetString (new RecursiveTestClass { Child = new RecursiveTestClass () })
            );
        }
        
        class PrivateTestClass
        {
            readonly string foo;
            
            public PrivateTestClass (string foo)
            {
                this.foo = foo;
            }
            
            #pragma warning disable 0169
            [XmlAttribute] string Foo { get { return foo; } }
            #pragma warning restore 0169
        }
        
        [Test]
        public void PrivateTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><PrivateTestClass Foo=""bar"" />",
                serializer.GetString (new PrivateTestClass ("bar"))
            );
        }
        
        class IXmlSerializableTypeTestClass : IXmlSerializable
        {
            public string Foo { get; set; }
            
            public void SerializeSelfAndMembers (XmlSerializationContext context)
            {
                context.Writer.WriteStartElement ("test");
                SerializeMembersOnly (context);
                context.Writer.WriteEndElement ();
            }
            
            public void SerializeMembersOnly (XmlSerializationContext context)
            {
                context.Writer.WriteAttributeString ("foo", Foo);
            }
        }
        
        [Test]
        public void IXmlSerializationTypeTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><test foo=""bar"" />",
                serializer.GetString (new IXmlSerializableTypeTestClass { Foo = "bar" })
            );
        }
        
        class IXmlSerializableMembersTestClass
        {
            [XmlElement] public IXmlSerializableTypeTestClass Info { get; set; }
        }
        
        [Test]
        public void IXmlSerializationMembersTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><IXmlSerializableMembersTestClass><Info foo=""bar"" /></IXmlSerializableMembersTestClass>",
                serializer.GetString (new IXmlSerializableMembersTestClass { Info = new IXmlSerializableTypeTestClass { Foo = "bar" } })
            );
        }
        
        class OverrideTestBaseClass
        {
            [XmlElement] public virtual string Foo { get; set; }
        }
        
        class OverrideTestSubClass : OverrideTestBaseClass
        {
            [XmlElement ("foo")] public override string Foo {
                get { return base.Foo; }
                set { base.Foo = value; }
            }
        }
        
        [Test]
        public void OverrideTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><OverrideTestSubClass><foo>bar</foo></OverrideTestSubClass>",
                serializer.GetString (new OverrideTestSubClass { Foo = "bar" })
            );
            
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><OverrideTestBaseClass><Foo>bar</Foo></OverrideTestBaseClass>",
                serializer.GetString<OverrideTestBaseClass> (new OverrideTestSubClass { Foo = "bar" })
            );
        }
        
        class OverrideOmitTestSubClass : OverrideTestBaseClass
        {
            public override string Foo {
                get { return base.Foo; }
                set { base.Foo = value; }
            }
            
            public override string ToString () { return string.Empty; }
        }
        
        [Test]
        public void OverrideOmitTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><OverrideOmitTestSubClass />",
                serializer.GetString (new OverrideOmitTestSubClass { Foo = "bar" })
            );
            
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><OverrideTestBaseClass><Foo>bar</Foo></OverrideTestBaseClass>",
                serializer.GetString<OverrideTestBaseClass> (new OverrideOmitTestSubClass { Foo = "bar" })
            );
        }
        
        [XmlType ("test")]
        class XmlSerializableTestBaseClass : XmlSerializable
        {
            [XmlAttribute] public virtual string Foo { get; set; }
            
            protected override void SerializeSelfAndMembers (XmlSerializationContext context)
            {
                context.AutoSerializeObjectAndMembers (this);
            }
            
            protected override void SerializeMembersOnly (XmlSerializationContext context)
            {
                context.AutoSerializeMembersOnly (this);
            }
        }
        
        class XmlSerializableTestSubClass : XmlSerializableTestBaseClass
        {
            [XmlAttribute ("foo")] public override string Foo {
                get { return base.Foo; }
                set { base.Foo = value; }
            }
            
            protected override void SerializeMembersOnly (XmlSerializationContext context)
            {
                context.AutoSerializeMembersOnly (this);
            }
        }
        
        [Test]
        public void XmlSerializableTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><test foo=""bar"" />",
                serializer.GetString (new XmlSerializableTestSubClass { Foo = "bar" })
            );
            
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><test foo=""bar"" />",
                serializer.GetString<XmlSerializableTestBaseClass> (new XmlSerializableTestSubClass { Foo = "bar" })
            );
        }
        
        class DoNotSerializeTestClass
        {
            [DoNotSerialize, XmlAttribute] public string Foo { get; set; }
            [DoNotSerialize, XmlElement] public string Bar { get; set; }
            [XmlAttribute] public string Bat { get; set; }
            [XmlElement] public string Baz { get; set; }
        }
        
        [Test]
        public void DoNotSerializeTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><DoNotSerializeTestClass Bat=""bat""><Baz>baz</Baz></DoNotSerializeTestClass>",
                serializer.GetString (new DoNotSerializeTestClass { Foo = "foo", Bar = "bar", Bat = "bat", Baz = "baz" })
            );
        }
        
        enum EnumTestEnum
        {
            [XmlEnum ("foo")] Foo,
            [XmlEnum ("bar")] Bar,
            [XmlEnum ("bat")] Bat
        }
        
        class EnumTestClass
        {
            [XmlAttribute] public EnumTestEnum Thing { get; set; }
            [XmlElement] public EnumTestEnum Doohicky { get; set; }
        }
        
        [Test]
        public void EnumTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><EnumTestClass Thing=""bar""><Doohicky>bat</Doohicky></EnumTestClass>",
                serializer.GetString (new EnumTestClass { Thing = EnumTestEnum.Bar, Doohicky = EnumTestEnum.Bat })
            );
        }
        
        [XmlNamespace ("udn:mono-upnp:foo", "f")]
        [XmlNamespace ("udn:mono-upnp:bar", "b")]
        class NamespaceTestClass
        {
            [XmlElement (Namespace = "udn:mono-upnp:foo")] public string Foo { get; set; }
            [XmlElement (Namespace = "udn:mono-upnp:bar")] public string Bar { get; set; }
        }
        
        [Test]
        public void NamespaceTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><NamespaceTestClass xmlns:b=""udn:mono-upnp:bar"" xmlns:f=""udn:mono-upnp:foo""><f:Foo>foo</f:Foo><b:Bar>bar</b:Bar></NamespaceTestClass>",
                serializer.GetString (new NamespaceTestClass { Foo = "foo", Bar = "bar" }));
        }
        
        class ValueTestClass
        {
            [XmlValue] public string Foo { get; set; }
        }
        
        [Test]
        public void ValueTest ()
        {
            Assert.AreEqual (
                @"<?xml version=""1.0"" encoding=""utf-8""?><ValueTestClass>bar</ValueTestClass>",
                serializer.GetString (new ValueTestClass { Foo = "bar" }));
        }
    }
}
