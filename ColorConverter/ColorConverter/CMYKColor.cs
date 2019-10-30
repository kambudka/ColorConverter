namespace ColorConverter
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class CmykColor
    {
        private readonly float _c;

        private readonly float _m;

        private readonly float _y;

        private readonly float _k;

        public CmykColor(float c, float m, float y, float k)
        {
            _c = c;
            _m = m;
            _y = y;
            _k = k;
        }

        public float C
        {
            get { return _c; }
        }

        public float M
        {
            get { return _m; }
        }

        public float Y
        {
            get { return _y; }
        }

        public float K
        {
            get { return _k; }
        }
    }
}
