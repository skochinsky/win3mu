using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Win3muCore
{
    public class StringPointer
    {
        public StringPointer(string str)
        {
            _str = str;
            _pos = 0;
        }

        string _str;
        int _pos;

        public int Position
        {
            get { return _pos; }
        }

        public string Remaining
        {
            get
            {
                return _str.Substring(_pos);
            }
        }

        bool IsIdentifierLeadChar(char ch)
        {
            return char.IsLetter(ch) || ch == '_';
        }

        bool IsIdentifierChar(char ch)
        {
            return char.IsLetterOrDigit(ch) || ch == '_';
        }


        public string SkipIdentifier()
        {
            if (!IsIdentifierLeadChar(this[0]))
                return null;

            int start = Position;
            _pos++;

            while (IsIdentifierChar(this[0]))
            {
                _pos++;
            }

            return Extract(start, _pos);
        }

        public string Extract(int posFrom, int posTo)
        {
            if (posTo <= posFrom)
                return "";
            return _str.Substring(posFrom, posTo - posFrom);
        }

        public char this[int offset]
        {
            get
            {
                int pos = _pos + offset;
                if (pos == _str.Length)
                    return '\0';
                else
                    return _str[pos];
            }
        }

        public static StringPointer operator ++(StringPointer p)
        {
            p._pos++;
            return p;
        }

        public bool Skip(char ch)
        {
            if (this[0] == ch)
            {
                _pos++;
                return true;

            }
            return false;
        }

        public bool Skip(string str)
        {
            if (_pos + str.Length <= _str.Length)
            {
                if (_str.Substring(_pos, str.Length) == str)
                {
                    _pos += str.Length;
                    return true;
                }
            }
            return false;
        }

        public bool SkipUntil(char ch)
        {
            int pos = _pos;
            while (pos < _str.Length)
            {
                if (_str[pos] == ch)
                {
                    _pos = pos;
                    return true;
                }
                pos++;
            }
            return false;
        }

        public bool SkipUntilOnOf(params char[] chars)
        {
            int pos = _pos;
            while (pos < _str.Length)
            {
                if (chars.Contains(_str[pos]))
                {
                    _pos = pos;
                    return true;
                }
                pos++;
            }
            return false;
        }

        public void SkipWhiteSpace()
        {
            while (char.IsWhiteSpace(this[0]))
                _pos++;
        }

        public bool EOL
        {
            get
            {
                char ch = this[0];
                return ch == '\r' || ch == '\n' || ch == '\0';
            }
        }

        public bool EOF
        {
            get
            {
                return _pos >= _str.Length;
            }
        }

        public void SkipEOL()
        {
            if (this[0] == '\r')
                _pos++;
            if (this[0] == '\n')
                _pos++;
        }

        public void SkipToEOL()
        {
            while (!EOL)
            {
                _pos++;
            }
        }

        public void SkipToNextLine()
        {
            SkipToEOL();
            SkipEOL();
        }

        public bool ParseInt(out int value)
        {
            bool found = false;
            value = 0;
            while (this[0] >= '0' && this[0] <= '9')
            {
                value = value * 10 + (this[0] - '0');
                found = true;
                _pos++;
            }
            return found;
        }
    }
}
