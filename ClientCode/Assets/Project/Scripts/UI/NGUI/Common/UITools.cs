using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace zb.NGUILibrary
{
    public static class UITools
    {
        /// <summary>
        /// UIScrollView移动到指定位置
        /// </summary>
        /// <param name="panel">UIPanel</param>
        /// <param name="scrollView">UIScrollView</param>
        /// <param name="target">目标物体</param>
        /// <param name="type">1.最上方/最左方 2.中心 3.最下方/最右方</param>
        /// <param name="vertical">垂直或者水平</param>
        /// <param name="width">单元格宽度(为0时自动计算)</param>
        /// <param name="high">单元格高度(为0时自动计算)</param>

        public static void OnScrollViewMove(UIPanel panel, UIScrollView scrollView, GameObject target, int type, bool vertical = true, float width = 0, float high = 0)
        {
            scrollView.ResetPosition();

            // 垂直
            if (vertical)
            {
                float _heigh = high;
                if (_heigh == 0)
                {
                    _heigh = NGUIMath.CalculateRelativeWidgetBounds(target.transform).size.y;
                }
                if (type == 1)
                {
                    scrollView.MoveRelative(Vector3.down * (target.transform.localPosition.y + _heigh / 2f));
                    scrollView.RestrictWithinBounds(true);
                }
                else if (type == 2)
                {
                    scrollView.MoveRelative(Vector3.down * (target.transform.localPosition.y + panel.baseClipRegion.w / 2f - _heigh / 2f + _heigh / 2f));
                    scrollView.RestrictWithinBounds(true);
                }
                else if (type == 3)
                {
                    scrollView.MoveRelative(Vector3.down * (target.transform.localPosition.y + panel.baseClipRegion.w - _heigh + _heigh / 2f));
                    scrollView.RestrictWithinBounds(true);
                }
            }
            // 水平
            else
            {
                float _width = width;
                if (_width == 0)
                {
                    _width = NGUIMath.CalculateRelativeWidgetBounds(target.transform).size.x;
                }

                if (type == 1)
                {
                    scrollView.MoveRelative(Vector3.left * target.transform.localPosition.x);
                    scrollView.RestrictWithinBounds(true);
                }
                else if (type == 2)
                {
                    scrollView.MoveRelative(Vector3.left * (target.transform.localPosition.x - panel.baseClipRegion.z / 2f + _width / 2f));
                    scrollView.RestrictWithinBounds(true);
                }
                else if (type == 3)
                {
                    scrollView.MoveRelative(Vector3.left * (target.transform.localPosition.x - panel.baseClipRegion.z + _width));
                    scrollView.RestrictWithinBounds(true);
                }
            }
        }
    }
}