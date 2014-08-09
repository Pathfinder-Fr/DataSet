// -----------------------------------------------------------------------
// <copyright file="HtmlExtensionsTests.cs" company="Pathfinder-fr">
// Copyright (c) Pathfinder-fr. Tous droits reserves.
// </copyright>
// -----------------------------------------------------------------------

namespace PathfinderDb.Views
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HtmlExtensionsTests
    {
        [TestMethod]
        public void GetValues_01()
        {

        }
        [TestMethod]
        public void IsValidForEnum_Flags()
        {
            bool isArray;
            bool isNullable;
            Type enumType;

            var isValid = HtmlExtensions.IsValidForEnum(typeof(MyFlagsEnum), out isArray, out isNullable, out enumType);

            Assert.IsFalse(isValid);
        }
        [TestMethod]
        public void IsValidForEnum_FlagsArray()
        {
            bool isArray;
            bool isNullable;
            Type enumType;

            var isValid = HtmlExtensions.IsValidForEnum(typeof(MyFlagsEnum[]), out isArray, out isNullable, out enumType);

            Assert.IsFalse(isValid);
        }

        [TestMethod]
        public void IsValidForEnum_NullableArray()
        {
            bool isArray;
            bool isNullable;
            Type enumType;

            var isValid = HtmlExtensions.IsValidForEnum(typeof(MyEnum?[]), out isArray, out isNullable, out enumType);

            Assert.IsTrue(isValid);
            Assert.IsTrue(isArray);
            Assert.IsTrue(isNullable);
            Assert.AreEqual(typeof(MyEnum), enumType);
        }

        [TestMethod]
        public void IsValidForEnum_Array()
        {
            bool isArray;
            bool isNullable;
            Type enumType;

            var isValid = HtmlExtensions.IsValidForEnum(typeof(MyEnum[]), out isArray, out isNullable, out enumType);

            Assert.IsTrue(isValid);
            Assert.IsTrue(isArray);
            Assert.IsFalse(isNullable);
            Assert.AreEqual(typeof(MyEnum), enumType);
        }

        [TestMethod]
        public void IsValidForEnum_Nullable()
        {
            bool isArray;
            bool isNullable;
            Type enumType;

            var isValid = HtmlExtensions.IsValidForEnum(typeof(MyEnum?), out isArray, out isNullable, out enumType);

            Assert.IsTrue(isValid);
            Assert.IsFalse(isArray);
            Assert.IsTrue(isNullable);
            Assert.AreEqual(typeof(MyEnum), enumType);
        }

        [TestMethod]
        public void IsValidForEnum_Default()
        {
            bool isArray;
            bool isNullable;
            Type enumType;

            var isValid = HtmlExtensions.IsValidForEnum(typeof(MyEnum), out isArray, out isNullable, out enumType);

            Assert.IsTrue(isValid);
            Assert.IsFalse(isArray);
            Assert.IsFalse(isNullable);
            Assert.AreEqual(typeof(MyEnum), enumType);
        }

        [TestMethod]
        public void GetSelectList_WithValues_01()
        {
            var selectList = HtmlExtensions.GetSelectList(typeof(MyEnum), true, MyEnum.One);

            Assert.AreEqual(5, selectList.Count);
            Assert.IsFalse(selectList[1].Selected);
            Assert.IsTrue(selectList[2].Selected);
            Assert.IsFalse(selectList[3].Selected);
        }

        [TestMethod]
        public void GetSelectList_WithValues_02()
        {
            var selectList = HtmlExtensions.GetSelectList(typeof(MyEnum), true, MyEnum.One, MyEnum.Two);

            Assert.AreEqual(5, selectList.Count);
            Assert.IsFalse(selectList[1].Selected);
            Assert.IsTrue(selectList[2].Selected);
            Assert.IsTrue(selectList[3].Selected);
        }

        [TestMethod]
        public void GetSelectList_Simple_01()
        {
            var selectList = HtmlExtensions.GetSelectList(typeof(MyEnum), false);

            Assert.AreEqual(4, selectList.Count);
        }

        [TestMethod]
        public void GetSelectList_Simple_02()
        {
            var selectList = HtmlExtensions.GetSelectList(typeof(MyEnum), false);

            Assert.AreEqual("Un", selectList[1].Text);
        }

        [TestMethod]
        public void GetSelectList_Simple_03()
        {
            var selectList = HtmlExtensions.GetSelectList(typeof(MyEnum), true);

            Assert.AreEqual(5, selectList.Count);
            Assert.AreEqual(string.Empty, selectList[0].Text);
        }

        class FakeViewDataContainer : IViewDataContainer
        {
            public FakeViewDataContainer()
            {
                this.ViewData = new ViewDataDictionary();
            }

            public ViewDataDictionary ViewData
            {
                get;
                set;
            }
        }
    }

    internal sealed class MyModel
    {
        public MyEnum[] Enum { get; set; }
    }

    internal enum MyEnum
    {
        Default,

        [Display(Name = "Un")]
        One,

        Two,

        Three
    }

    [Flags]
    internal enum MyFlagsEnum
    {
        Default,

        One,

        Two,
    }
}