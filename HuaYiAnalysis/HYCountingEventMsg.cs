﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:2.0.50727.5466
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=2.0.50727.1432.
// 
namespace SuperDeviceFactory.CountMsg
{


    /// <summary>
    /// 上报人流
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class CountingEventMsg
    {

        private string alarmEventVersionField;

        private string deviceIDField;

        private string feedNumberField;

        private string feedNicknameField;

        private string stageCycleField;

        private string currentCycleInField;

        private string currentCycleOutField;

        private string currentHourInField;

        private string currentHourOutField;

        private string currentDayInField;

        private string currentDayOutField;

        /// <summary>
        /// 版本信息
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string AlarmEventVersion
        {
            get
            {
                return this.alarmEventVersionField;
            }
            set
            {
                this.alarmEventVersionField = value;
            }
        }

        /// <summary>
        /// BW设备标识
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string DeviceID
        {
            get
            {
                return this.deviceIDField;
            }
            set
            {
                this.deviceIDField = value;
            }
        }
        /// <summary>
        /// 分析通道号
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FeedNumber
        {
            get
            {
                return this.feedNumberField;
            }
            set
            {
                this.feedNumberField = value;
            }
        }
        /// <summary>
        /// 通道标识
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FeedNickname
        {
            get
            {
                return this.feedNicknameField;
            }
            set
            {
                this.feedNicknameField = value;
            }
        }

        /// <summary>
        /// 统计周期，默认30秒一个周期
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string StageCycle
        {
            get
            {
                return this.stageCycleField;
            }
            set
            {
                this.stageCycleField = value;
            }
        }

        /// <summary>
        /// 当前周期进入人数
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CurrentCycleIn
        {
            get
            {
                return this.currentCycleInField;
            }
            set
            {
                this.currentCycleInField = value;
            }
        }

        /// <summary>
        /// 当前周期出去人数
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CurrentCycleOut
        {
            get
            {
                return this.currentCycleOutField;
            }
            set
            {
                this.currentCycleOutField = value;
            }
        }

        /// <summary>
        /// 当前小时进入人数
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CurrentHourIn
        {
            get
            {
                return this.currentHourInField;
            }
            set
            {
                this.currentHourInField = value;
            }
        }

       /// <summary>
       /// 当前小时出去人数
       /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CurrentHourOut
        {
            get
            {
                return this.currentHourOutField;
            }
            set
            {
                this.currentHourOutField = value;
            }
        }

        /// <summary>
        /// 当天进入人数
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CurrentDayIn
        {
            get
            {
                return this.currentDayInField;
            }
            set
            {
                this.currentDayInField = value;
            }
        }

        /// <summary>
        /// 当天出去人数
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string CurrentDayOut
        {
            get
            {
                return this.currentDayOutField;
            }
            set
            {
                this.currentDayOutField = value;
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NewDataSet
    {

        private CountingEventMsg[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("CountingEventMsg")]
        public CountingEventMsg[] Items
        {
            get
            {
                return this.itemsField;
            }
            set
            {
                this.itemsField = value;
            }
        }
    }
}
