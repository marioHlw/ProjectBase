using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace zb.UGUILibrary
{
    public class UISystemEditor : EditorWindow
    {
        private static readonly string BEGIN_WRITING_YOUR_CODE = "///<<< BEGIN WRITING YOUR CODE";
        private static readonly string END_WRITING_YOUR_CODE = "///<<< END WRITING YOUR CODE";

        private static readonly string FORM_PATH_TEST = "Assets/Tools/UGUI/System/Module/";
        private static readonly string FORM_PATH = "Assets/Project/Scripts/UI/UGUI/GameSystem/";

        private static string formName = null;              // 窗口名字
        private static string modelName = null;             // 窗口管理脚本名字
        private static Transform rootTransform;             // 选择的窗口
        private static Dictionary<string, Transform> m_childMap = new Dictionary<string, Transform>();

        // 名字对应的组件类型，用来初始化创建变量，注意开始只能是5个字符，如果想不同的字符，自行扩展下面的代码。
        private static Dictionary<string, Type> m_titleToComponent = new Dictionary<string, Type>
        {
            { "u_txt", typeof(Text)},
            { "u_sld", typeof(Slider) },
        };

        [MenuItem("Tools/UGUI/创建\\更新系统", false, 3)]
        private static void Open()
        {
            if (Selection.activeGameObject.name.StartsWith("BLK_"))
            {
                formName = Selection.activeGameObject.name.Substring(4);
                rootTransform = Selection.activeGameObject.transform;
                modelName = Selection.activeGameObject.name;

                string _path = Application.dataPath.Substring(0, Application.dataPath.Length - 6) + FORM_PATH + formName;
                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                    AssetDatabase.Refresh();
                }

                CreateModule(formName);
                CreateData(formName);
                CreateProxy(formName);
                CreateUIForm(formName);
                CreateUIGroup(formName);
            }
        }

        private static void CreateModule(string moduleName)
        {
            string _moduleCode = null;
            string _modulePath = FORM_PATH + moduleName + "/UI" + moduleName + "Modlue.cs";
            string _testPath = FORM_PATH_TEST + "UITestModule.cs";
            TextAsset _moduleTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_modulePath);
            TextAsset _testTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_testPath);

            _moduleCode = _testTextAsset.text;

            if (_moduleTextAsset != null)
            {
                int _beginIndex = 0;
                int _endIndex = 0;

                // using
                string _strUsing = GetWritingYourCode(_moduleTextAsset.text, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", out _beginIndex, out _endIndex);
                _moduleCode = Replace(_moduleCode, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", _strUsing);

                // your code
                string _strCode = GetWritingYourCode(_moduleTextAsset.text, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", out _beginIndex, out _endIndex);
                _moduleCode = Replace(_moduleCode, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", _strCode);
            }

            _moduleCode = _moduleCode.Replace("Test", moduleName);

            Save(_modulePath, _moduleCode);
        }

        private static void CreateData(string moduleName)
        {
            string _dataCode = null;
            string _dataPath = FORM_PATH + moduleName + "/UI" + moduleName + "Data.cs";
            string _testPath = FORM_PATH_TEST + "UITestData.cs";
            TextAsset _dataTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_dataPath);
            TextAsset _testTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_testPath);

            _dataCode = _testTextAsset.text;

            if (_dataTextAsset != null)
            {
                int _beginIndex = 0;
                int _endIndex = 0;

                // using
                string _strUsing = GetWritingYourCode(_dataTextAsset.text, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", out _beginIndex, out _endIndex);
                _dataCode = Replace(_dataCode, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", _strUsing);

                // your code
                string _strCode = GetWritingYourCode(_dataTextAsset.text, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", out _beginIndex, out _endIndex);
                _dataCode = Replace(_dataCode, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", _strCode);
            }

            _dataCode = _dataCode.Replace("Test", moduleName);

            Save(_dataPath, _dataCode);
        }

        private static void CreateProxy(string moduleName)
        {
            string _proxyCode = null;
            string _proxyPath = FORM_PATH + moduleName + "/UI" + moduleName + "Proxy.cs";
            string _testPath = FORM_PATH_TEST + "UITestProxy.cs";
            TextAsset _proxyTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_proxyPath);
            TextAsset _testTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_testPath);

            _proxyCode = _testTextAsset.text;

            if (_proxyTextAsset != null)
            {
                int _beginIndex = 0;
                int _endIndex = 0;

                // using
                string _strUsing = GetWritingYourCode(_proxyTextAsset.text, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", out _beginIndex, out _endIndex);
                _proxyCode = Replace(_proxyCode, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", _strUsing);

                // your code
                string _strCode = GetWritingYourCode(_proxyTextAsset.text, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", out _beginIndex, out _endIndex);
                _proxyCode = Replace(_proxyCode, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", _strCode);
            }

            _proxyCode = _proxyCode.Replace("Test", moduleName);

            Save(_proxyPath, _proxyCode);
        }

        private static void CreateUIForm(string moduleName)
        {
            string _formCode = null;
            string _formPath = FORM_PATH + moduleName + "/BLK_UIForm" + moduleName + ".cs";
            string _testPath = FORM_PATH_TEST + "BLK_UIFormTest.cs";
            TextAsset _formTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_formPath);
            TextAsset _testTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_testPath);

            _formCode = _testTextAsset.text;

            if (_formTextAsset != null)
            {
                int _beginIndex = 0;
                int _endIndex = 0;

                // using
                string _strUsing = GetWritingYourCode(_formTextAsset.text, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", out _beginIndex, out _endIndex);
                _formCode = Replace(_formCode, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", _strUsing);

                // your code
                string _strCode = GetWritingYourCode(_formTextAsset.text, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", out _beginIndex, out _endIndex);
                _formCode = Replace(_formCode, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", _strCode);

                // 创建UI组件变量
                m_childMap.Clear();
                GetChilds(rootTransform);
                string _str1 = "";  // 所有变量
                string _str2 = "";  // 获取所有变量
                int _count = 0;
                foreach (KeyValuePair<string, Transform> temp in m_childMap)
                {
                    string _key = temp.Key.Substring(0, 5);
                    if (m_titleToComponent.ContainsKey(_key))
                    {
                        if (_count == 0)
                        {
                            _str1 = _str1 + string.Format("private {0} {1};", m_titleToComponent[_key].Name, temp.Key) + "\n";
                        }
                        else if (_count == m_childMap.Count - 1)
                        {
                            _str1 = _str1 + string.Format("    private {0} {1};", m_titleToComponent[_key].Name, temp.Key);
                        }
                        else
                        {
                            _str1 = _str1 + string.Format("    private {0} {1};", m_titleToComponent[_key].Name, temp.Key) + "\n";
                        }
                        _count++;
                        if (m_titleToComponent[_key].Name == "GameObject")
                        {
                            _str2 += (string.Format("        {0} = transform.Find(\"{1}\").gameObject;\n", temp.Key, GetTransformPath(temp.Value)));
                        }
                        else if (m_titleToComponent[_key].Name == "Transform")
                        {
                            _str2 += (string.Format("        {0} = transform.Find(\"{1}\");\n", temp.Key, GetTransformPath(temp.Value)));
                        }
                        else
                        {
                            _str2 += (string.Format("        {0} = transform.Find(\"{1}\").GetComponent<{2}>();\n", temp.Key, GetTransformPath(temp.Value), m_titleToComponent[_key].Name));
                        }
                    }
                }
                _str1 += "\n\n";
                _str1 += "    private void OnFindChilds()\n";
                _str1 += "    {\n";
                _str1 += _str2;
                _str1 += "    }";
                _formCode = _formCode.Replace("//variable", _str1);
            }

            _formCode = _formCode.Replace("Test", moduleName);

            Save(_formPath, _formCode);
        }

        private static void CreateUIGroup(string moduleName)
        {
            string _proxyCode = null;
            string _proxyPath = FORM_PATH + moduleName + "/BLK_UIGroup" + moduleName + ".cs";
            string _testPath = FORM_PATH_TEST + "BLK_UIGroupTest.cs";
            TextAsset _proxyTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_proxyPath);
            TextAsset _testTextAsset = AssetDatabase.LoadAssetAtPath<TextAsset>(_testPath);

            _proxyCode = _testTextAsset.text;

            if (_proxyTextAsset != null)
            {
                int _beginIndex = 0;
                int _endIndex = 0;

                // using
                string _strUsing = GetWritingYourCode(_proxyTextAsset.text, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", out _beginIndex, out _endIndex);
                _proxyCode = Replace(_proxyCode, BEGIN_WRITING_YOUR_CODE + " USING", END_WRITING_YOUR_CODE + " USING", _strUsing);

                // your code
                string _strCode = GetWritingYourCode(_proxyTextAsset.text, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", out _beginIndex, out _endIndex);
                _proxyCode = Replace(_proxyCode, BEGIN_WRITING_YOUR_CODE + " CORE", END_WRITING_YOUR_CODE + " CORE", _strCode);
            }

            _proxyCode = _proxyCode.Replace("Test", moduleName);

            Save(_proxyPath, _proxyCode);
        }

        /// <summary>
        /// 获取添加的代码
        /// </summary>
        /// <param name="target">源字符串</param>
        /// <param name="begin">开始标识</param>
        /// <param name="end">结束标识</param>

        private static string GetWritingYourCode(string target, string begin, string end, out int beginIndex, out int endIndex)
        {
            byte[] _bytes = new byte[] { };
            string _code = null;
            beginIndex = 0;
            endIndex = 0;

            if (target != null)
            {
                beginIndex = target.IndexOf(begin);
                endIndex = target.IndexOf(end);

                if (beginIndex > -1 && endIndex > -1)
                {
                    if (beginIndex > endIndex)
                    {
                        beginIndex = beginIndex + endIndex;
                        endIndex = beginIndex - endIndex;
                        beginIndex = beginIndex - endIndex;
                    }
                    _code = target.Substring(beginIndex + begin.Length, endIndex - beginIndex - end.Length - 2);
                    Utility.StringToUTF8Bytes(_code, ref _bytes);
                }
            }
            return _code;
        }

        /// <summary>
        /// 替换字符串
        /// </summary>
        /// <param name="target">需要替换的字符串</param>
        /// <param name="begin">开始标识字符串</param>
        /// <param name="end">结束标识字符串</param>
        /// <param name="str">替换字符串</param>

        private static string Replace(string target, string begin, string end, string str)
        {
            byte[] _bytes = new byte[] { };
            string _code = null;
            int beginIndex = 0;
            int endIndex = 0;

            if (target != null)
            {
                beginIndex = target.IndexOf(begin);
                endIndex = target.IndexOf(end);

                if (beginIndex > -1 && endIndex > -1)
                {
                    if (beginIndex > endIndex)
                    {
                        beginIndex = beginIndex + endIndex;
                        endIndex = beginIndex - endIndex;
                        beginIndex = beginIndex - endIndex;
                    }
                    _code = target.Substring(beginIndex + begin.Length, endIndex - beginIndex - end.Length - 2);
                    Utility.StringToUTF8Bytes(_code, ref _bytes);
                }

                target = target.Remove(beginIndex + begin.Length, endIndex - beginIndex - end.Length - 2);
                target = target.Insert(beginIndex + begin.Length, str);
            }

            return target;
        }

        private static void Save(string outPutFile, string uiScript)
        {
            StreamWriter sw = new StreamWriter(outPutFile, false);

            sw.Write(uiScript);

            sw.Close();

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void GetChilds(Transform trans)
        {
            if (trans.childCount > 0)
            {
                for (int i = 0; i < trans.childCount; i++)
                {
                    GetChilds(trans.GetChild(i));
                }
            }

            if (trans.gameObject.name.StartsWith("u_"))
            {
                if (m_childMap.ContainsKey(trans.gameObject.name))
                {
                    Debug.LogError(GetTransformPath(trans) + "  跟其他的名字重复，请使用不同命名。");
                }
                else
                {
                    m_childMap.Add(trans.gameObject.name, trans);
                }
            }
        }

        private static string GetTransformPath(Transform trans)
        {
            if (rootTransform != null)
            {
                string _path = trans.name;
                Transform _partent = trans.parent;
                while (_partent != rootTransform)
                {
                    _path = _path.Insert(0, "/");
                    _path = _path.Insert(0, _partent.name);
                    _partent = _partent.parent;
                }

                return _path;
            }

            return "";
        }
    }
}