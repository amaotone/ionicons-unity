using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using UnityEngine.UI;

namespace Ionicons
{
    public class IonicText : Text
    {
        bool disableDirty;
        Regex iconRegExp = new Regex(@"<(ion-.*?)>");

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            string cache = text;
            disableDirty = true;

            text = Decode(text);
            base.OnPopulateMesh(toFill);
            text = cache;

            disableDirty = false;
        }

        string Decode(string value)
        {
            return iconRegExp.Replace(value, m =>
            {
                var iconName = m.Groups[1].Value;
                if (Ionicon.Table.ContainsKey(iconName))
                {
                    return ((char)int.Parse(Ionicon.Table[iconName], NumberStyles.HexNumber)).ToString();
                }
                return m.Groups[0].Value;
            });
        }

        public override void SetLayoutDirty()
        {
            if (disableDirty)
                return;
            base.SetLayoutDirty();
        }

        public override void SetVerticesDirty()
        {
            if (disableDirty)
                return;
            base.SetVerticesDirty();
        }

        public override void SetMaterialDirty()
        {
            if (disableDirty)
                return;
            base.SetMaterialDirty();
        }
    }
}