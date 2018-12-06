using System;
using System.Collections.Generic;
using System.Text;

namespace SuperDeviceFactory.LayoutHelper
{

    /// <summary>
    /// 轨迹信息
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class XMLLayoutMessage
    {

        private string layoutVersionField;

        private string deviceIDField;
        private string feedNumberField;

        private string feedNicknameField;

        private string frameIDField;

        private string frameSizeField;

        private XMLLayoutMessageLayoutElementListLayoutElement[] layoutElementListField;

        /// <summary>
        /// 版本信息
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string LayoutVersion
        {
            get
            {
                return this.layoutVersionField;
            }
            set
            {
                this.layoutVersionField = value;
            }
        }

        /// <summary>
        /// 设备标识
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
        /// 帧序号；指明轨迹对应的帧序号；与“媒体数据回调通知结构体”定义的帧序号一致。
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FrameID
        {
            get
            {
                return this.frameIDField;
            }
            set
            {
                this.frameIDField = value;
            }
        }


        /// <summary>
        /// 帧大小；格式为：长度,宽度。
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string FrameSize
        {
            get
            {
                return this.frameSizeField;
            }
            set
            {
                this.frameSizeField = value;
            }
        }

       /// <summary>
       /// 轨迹层列表
       /// </summary>
        [System.Xml.Serialization.XmlArrayAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        [System.Xml.Serialization.XmlArrayItemAttribute("LayoutElement", typeof(XMLLayoutMessageLayoutElementListLayoutElement), Form = System.Xml.Schema.XmlSchemaForm.Unqualified, IsNullable = false)]
        public XMLLayoutMessageLayoutElementListLayoutElement[] LayoutElementList
        {
            get
            {
                return this.layoutElementListField;
            }
            set
            {
                this.layoutElementListField = value;
            }
        }
    }

    /// <summary>
    /// 轨迹层信息
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XMLLayoutMessageLayoutElementListLayoutElement
    {

        private string layerField;

        private string colorField;

        private string elementTypeField;

        private XMLLayoutMessageLayoutElementListLayoutElementTheText theTextField;

        private XMLLayoutMessageLayoutElementListLayoutElementThePoints thePointsField;

        /// <summary>
        /// 层类型；Alarmed：报警信息层 ROI：分析规则信息层
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Layer
        {
            get
            {
                return this.layerField;
            }
            set
            {
                this.layerField = value;
            }
        }

        /// <summary>
        /// 轨迹绘制颜色
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Color
        {
            get
            {
                return this.colorField;
            }
            set
            {
                this.colorField = value;
            }
        }

        /// <summary>
        /// 轨迹层元素类型：Text：文本Polyline：折线
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ElementType
        {
            get
            {
                return this.elementTypeField;
            }
            set
            {
                this.elementTypeField = value;
            }
        }

        /// <summary>
        /// 文本元素；当ElementType为Text有效
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("TheText", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public XMLLayoutMessageLayoutElementListLayoutElementTheText TheText
        {
            get
            {
                return this.theTextField;
            }
            set
            {
                this.theTextField = value;
            }
        }

        /// <summary>
        /// 折线元素；当ElementType为Polyline有效；注：坐标原点为画面的左上角
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute("ThePoints", Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public XMLLayoutMessageLayoutElementListLayoutElementThePoints ThePoints
        {
            get
            {
                return this.thePointsField;
            }
            set
            {
                this.thePointsField = value;
            }
        }
    }

    /// <summary>
    /// 文本元素信息
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XMLLayoutMessageLayoutElementListLayoutElementTheText
    {
        private string trackIDField;

        private string textField;

        private string scaleField;

        private string pointToDrawField;


        /// <summary>
        /// 目标索引，当Layer为Alarmed起效，ROI时默认-1
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TrackID
        {
            get
            {
                return this.trackIDField;
            }
            set
            {
                this.trackIDField = value;
            }
        }

        /// <summary>
        /// 回执的文本内容
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Text
        {
            get
            {
                return this.textField;
            }
            set
            {
                this.textField = value;
            }
        }

        /// <summary>
        /// 放大或缩小的比例
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string Scale
        {
            get
            {
                return this.scaleField;
            }
            set
            {
                this.scaleField = value;
            }
        }

        /// <summary>
        /// 文本左上角坐标；格式为：x,y；注：坐标原点为画面的左上角。
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string PointToDraw
        {
            get
            {
                return this.pointToDrawField;
            }
            set
            {
                this.pointToDrawField = value;
            }
        }
    }

    /// <summary>
    /// 折线元素信息
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class XMLLayoutMessageLayoutElementListLayoutElementThePoints
    {
        private string trackIDField;

        private string numberOfPointsField;

        private string elementPointsField;
        /// <summary>
        /// 目标索引，当Layer为Alarmed起效，ROI时默认-1
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string TrackID
        {
            get
            {
                return this.trackIDField;
            }
            set
            {
                this.trackIDField = value;
            }
        }

        /// <summary>
        /// 折线点坐标个数
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string NumberOfPoints
        {
            get
            {
                return this.numberOfPointsField;
            }
            set
            {
                this.numberOfPointsField = value;
            }
        }

        /// <summary>
        /// 折线点坐标列表；格式为：x,y;x1,y1表示从坐标x,y画线到坐标x1,y1
        /// </summary>
        [System.Xml.Serialization.XmlElementAttribute(Form = System.Xml.Schema.XmlSchemaForm.Unqualified)]
        public string ElementPoints
        {
            get
            {
                return this.elementPointsField;
            }
            set
            {
                this.elementPointsField = value;
            }
        }
    }


    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.1432")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NewDataSet
    {
        private XMLLayoutMessage[] itemsField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("XMLLayoutMessage")]
        public XMLLayoutMessage[] Items
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
