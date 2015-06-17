using System;

namespace Bardock.Utils.Extensions
{
    public static class CharExtensions
    {
        public static char ToLower(this char c)
        {
            return Char.ToLower(c);
        }

        public static bool IsAlphabetic(this char c)
        {
            return c >= 'a' && c <= 'z'
                || c >= 'A' && c <= 'Z';
        }

        public static bool IsNumeric(this char c)
        {
            return c >= '0' && c <= '9';
        }

        public static bool IsAlphaNumeric(this char c)
        {
            return c.IsAlphabetic() || c.IsNumeric();
        }

        public static bool IsValidUriPathSegment(this char c)
        {
            // Valid path segment characters: a-z A-Z 0-9 . - _ ~ ! $ & ' ( ) * + , ; = : @
            // See http://tools.ietf.org/html/rfc3986#section-3.3

            return c >= 'a' && c <= 'z'
                || c >= 'A' && c <= 'Z'
                || c >= '0' && c <= '9'
                || c.In('.', '-', '_', '~', '!', '$', '&', '\'', '(', ')', '*', '+', ',', ';', '=', ':', '@');
        }

        public static char FoldDiacritical(this char c)
        {
            switch (c)
            {
                case '\u00C0':
                    return '\u0041'; // A › A LATIN CAPITAL LETTER A WITH GRAVE › LATIN CAPITAL LETTER A
                case '\u00C1':
                    return '\u0041'; // Á › A LATIN CAPITAL LETTER A WITH ACUTE › LATIN CAPITAL LETTER A
                case '\u00C2':
                    return '\u0041'; // Â › A LATIN CAPITAL LETTER A WITH CIRCUMFLEX › LATIN CAPITAL LETTER A
                case '\u00C3':
                    return '\u0041'; // A › A LATIN CAPITAL LETTER A WITH TILDE › LATIN CAPITAL LETTER A
                case '\u00C4':
                    return '\u0041'; // Ä › A LATIN CAPITAL LETTER A WITH DIAERESIS › LATIN CAPITAL LETTER A
                case '\u00C5':
                    return '\u0041'; // A › A LATIN CAPITAL LETTER A WITH RING ABOVE › LATIN CAPITAL LETTER A
                case '\u0100':
                    return '\u0041'; // A › A LATIN CAPITAL LETTER A WITH MACRON › LATIN CAPITAL LETTER A
                case '\u0102':
                    return '\u0041'; // Ă › A LATIN CAPITAL LETTER A WITH BREVE › LATIN CAPITAL LETTER A
                case '\u0104':
                    return '\u0041'; // Ą › A LATIN CAPITAL LETTER A WITH OGONEK › LATIN CAPITAL LETTER A
                case '\u01CD':
                    return '\u0041'; // A › A LATIN CAPITAL LETTER A WITH CARON › LATIN CAPITAL LETTER A
                case '\u01DE':
                    return '\u0041'; // A › A LATIN CAPITAL LETTER A WITH DIAERESIS AND MACRON › LATIN CAPITAL LETTER A
                case '\u01E0':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH DOT ABOVE AND MACRON › LATIN CAPITAL LETTER A
                case '\u01FA':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH RING ABOVE AND ACUTE › LATIN CAPITAL LETTER A
                case '\u0200':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH DOUBLE GRAVE › LATIN CAPITAL LETTER A
                case '\u0202':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH INVERTED BREVE › LATIN CAPITAL LETTER A
                case '\u0226':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH DOT ABOVE › LATIN CAPITAL LETTER A
                case '\u1E00':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH RING BELOW › LATIN CAPITAL LETTER A
                case '\u1EA0':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH DOT BELOW › LATIN CAPITAL LETTER A
                case '\u1EA2':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH HOOK ABOVE › LATIN CAPITAL LETTER A
                case '\u1EA4':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND ACUTE › LATIN CAPITAL LETTER A
                case '\u1EA6':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND GRAVE › LATIN CAPITAL LETTER A
                case '\u1EA8':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE › LATIN CAPITAL LETTER A
                case '\u1EAA':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND TILDE › LATIN CAPITAL LETTER A
                case '\u1EAC':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH CIRCUMFLEX AND DOT BELOW › LATIN CAPITAL LETTER A
                case '\u1EAE':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH BREVE AND ACUTE › LATIN CAPITAL LETTER A
                case '\u1EB0':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH BREVE AND GRAVE › LATIN CAPITAL LETTER A
                case '\u1EB2':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH BREVE AND HOOK ABOVE › LATIN CAPITAL LETTER A
                case '\u1EB4':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH BREVE AND TILDE › LATIN CAPITAL LETTER A
                case '\u1EB6':
                    return '\u0041'; // ? › A LATIN CAPITAL LETTER A WITH BREVE AND DOT BELOW › LATIN CAPITAL LETTER A
                case '\u0181':
                    return '\u0042'; // ? › B LATIN CAPITAL LETTER B WITH HOOK › LATIN CAPITAL LETTER B
                case '\u0182':
                    return '\u0042'; // ? › B LATIN CAPITAL LETTER B WITH TOPBAR › LATIN CAPITAL LETTER B
                case '\u1E02':
                    return '\u0042'; // ? › B LATIN CAPITAL LETTER B WITH DOT ABOVE › LATIN CAPITAL LETTER B
                case '\u1E04':
                    return '\u0042'; // ? › B LATIN CAPITAL LETTER B WITH DOT BELOW › LATIN CAPITAL LETTER B
                case '\u1E06':
                    return '\u0042'; // ? › B LATIN CAPITAL LETTER B WITH LINE BELOW › LATIN CAPITAL LETTER B
                case '\u00C7':
                    return '\u0043'; // Ç › C LATIN CAPITAL LETTER C WITH CEDILLA › LATIN CAPITAL LETTER C
                case '\u0106':
                    return '\u0043'; // Ć › C LATIN CAPITAL LETTER C WITH ACUTE › LATIN CAPITAL LETTER C
                case '\u0108':
                    return '\u0043'; // C › C LATIN CAPITAL LETTER C WITH CIRCUMFLEX › LATIN CAPITAL LETTER C
                case '\u010A':
                    return '\u0043'; // C › C LATIN CAPITAL LETTER C WITH DOT ABOVE › LATIN CAPITAL LETTER C
                case '\u010C':
                    return '\u0043'; // Č › C LATIN CAPITAL LETTER C WITH CARON › LATIN CAPITAL LETTER C
                case '\u0187':
                    return '\u0043'; // ? › C LATIN CAPITAL LETTER C WITH HOOK › LATIN CAPITAL LETTER C
                case '\u1E08':
                    return '\u0043'; // ? › C LATIN CAPITAL LETTER C WITH CEDILLA AND ACUTE › LATIN CAPITAL LETTER C
                case '\u010E':
                    return '\u0044'; // Ď › D LATIN CAPITAL LETTER D WITH CARON › LATIN CAPITAL LETTER D
                case '\u0110':
                    return '\u0044'; // Đ › D LATIN CAPITAL LETTER D WITH STROKE › LATIN CAPITAL LETTER D
                case '\u018A':
                    return '\u0044'; // ? › D LATIN CAPITAL LETTER D WITH HOOK › LATIN CAPITAL LETTER D
                case '\u018B':
                    return '\u0044'; // ? › D LATIN CAPITAL LETTER D WITH TOPBAR › LATIN CAPITAL LETTER D
                case '\u1E0A':
                    return '\u0044'; // ? › D LATIN CAPITAL LETTER D WITH DOT ABOVE › LATIN CAPITAL LETTER D
                case '\u1E0C':
                    return '\u0044'; // ? › D LATIN CAPITAL LETTER D WITH DOT BELOW › LATIN CAPITAL LETTER D
                case '\u1E0E':
                    return '\u0044'; // ? › D LATIN CAPITAL LETTER D WITH LINE BELOW › LATIN CAPITAL LETTER D
                case '\u1E10':
                    return '\u0044'; // ? › D LATIN CAPITAL LETTER D WITH CEDILLA › LATIN CAPITAL LETTER D
                case '\u1E12':
                    return '\u0044'; // ? › D LATIN CAPITAL LETTER D WITH CIRCUMFLEX BELOW › LATIN CAPITAL LETTER D
                case '\u00C8':
                    return '\u0045'; // E › E LATIN CAPITAL LETTER E WITH GRAVE › LATIN CAPITAL LETTER E
                case '\u00C9':
                    return '\u0045'; // É › E LATIN CAPITAL LETTER E WITH ACUTE › LATIN CAPITAL LETTER E
                case '\u00CA':
                    return '\u0045'; // E › E LATIN CAPITAL LETTER E WITH CIRCUMFLEX › LATIN CAPITAL LETTER E
                case '\u00CB':
                    return '\u0045'; // Ë › E LATIN CAPITAL LETTER E WITH DIAERESIS › LATIN CAPITAL LETTER E
                case '\u0112':
                    return '\u0045'; // E › E LATIN CAPITAL LETTER E WITH MACRON › LATIN CAPITAL LETTER E
                case '\u0114':
                    return '\u0045'; // E › E LATIN CAPITAL LETTER E WITH BREVE › LATIN CAPITAL LETTER E
                case '\u0116':
                    return '\u0045'; // E › E LATIN CAPITAL LETTER E WITH DOT ABOVE › LATIN CAPITAL LETTER E
                case '\u0118':
                    return '\u0045'; // Ę › E LATIN CAPITAL LETTER E WITH OGONEK › LATIN CAPITAL LETTER E
                case '\u011A':
                    return '\u0045'; // Ě › E LATIN CAPITAL LETTER E WITH CARON › LATIN CAPITAL LETTER E
                case '\u0204':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH DOUBLE GRAVE › LATIN CAPITAL LETTER E
                case '\u0206':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH INVERTED BREVE › LATIN CAPITAL LETTER E
                case '\u0228':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CEDILLA › LATIN CAPITAL LETTER E
                case '\u1E14':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH MACRON AND GRAVE › LATIN CAPITAL LETTER E
                case '\u1E16':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH MACRON AND ACUTE › LATIN CAPITAL LETTER E
                case '\u1E18':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CIRCUMFLEX BELOW › LATIN CAPITAL LETTER E
                case '\u1E1A':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH TILDE BELOW › LATIN CAPITAL LETTER E
                case '\u1E1C':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CEDILLA AND BREVE › LATIN CAPITAL LETTER E
                case '\u1EB8':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH DOT BELOW › LATIN CAPITAL LETTER E
                case '\u1EBA':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH HOOK ABOVE › LATIN CAPITAL LETTER E
                case '\u1EBC':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH TILDE › LATIN CAPITAL LETTER E
                case '\u1EBE':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND ACUTE › LATIN CAPITAL LETTER E
                case '\u1EC0':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND GRAVE › LATIN CAPITAL LETTER E
                case '\u1EC2':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE › LATIN CAPITAL LETTER E
                case '\u1EC4':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND TILDE › LATIN CAPITAL LETTER E
                case '\u1EC6':
                    return '\u0045'; // ? › E LATIN CAPITAL LETTER E WITH CIRCUMFLEX AND DOT BELOW › LATIN CAPITAL LETTER E
                case '\u0191':
                    return '\u0046'; // F › F LATIN CAPITAL LETTER F WITH HOOK › LATIN CAPITAL LETTER F
                case '\u1E1E':
                    return '\u0046'; // ? › F LATIN CAPITAL LETTER F WITH DOT ABOVE › LATIN CAPITAL LETTER F
                case '\u011C':
                    return '\u0047'; // G › G LATIN CAPITAL LETTER G WITH CIRCUMFLEX › LATIN CAPITAL LETTER G
                case '\u011E':
                    return '\u0047'; // G › G LATIN CAPITAL LETTER G WITH BREVE › LATIN CAPITAL LETTER G
                case '\u0120':
                    return '\u0047'; // G › G LATIN CAPITAL LETTER G WITH DOT ABOVE › LATIN CAPITAL LETTER G
                case '\u0122':
                    return '\u0047'; // G › G LATIN CAPITAL LETTER G WITH CEDILLA › LATIN CAPITAL LETTER G
                case '\u0193':
                    return '\u0047'; // ? › G LATIN CAPITAL LETTER G WITH HOOK › LATIN CAPITAL LETTER G
                case '\u01E4':
                    return '\u0047'; // G › G LATIN CAPITAL LETTER G WITH STROKE › LATIN CAPITAL LETTER G
                case '\u01E6':
                    return '\u0047'; // G › G LATIN CAPITAL LETTER G WITH CARON › LATIN CAPITAL LETTER G
                case '\u01F4':
                    return '\u0047'; // ? › G LATIN CAPITAL LETTER G WITH ACUTE › LATIN CAPITAL LETTER G
                case '\u1E20':
                    return '\u0047'; // ? › G LATIN CAPITAL LETTER G WITH MACRON › LATIN CAPITAL LETTER G
                case '\u0124':
                    return '\u0048'; // H › H LATIN CAPITAL LETTER H WITH CIRCUMFLEX › LATIN CAPITAL LETTER H
                case '\u0126':
                    return '\u0048'; // H › H LATIN CAPITAL LETTER H WITH STROKE › LATIN CAPITAL LETTER H
                case '\u021E':
                    return '\u0048'; // ? › H LATIN CAPITAL LETTER H WITH CARON › LATIN CAPITAL LETTER H
                case '\u1E22':
                    return '\u0048'; // ? › H LATIN CAPITAL LETTER H WITH DOT ABOVE › LATIN CAPITAL LETTER H
                case '\u1E24':
                    return '\u0048'; // ? › H LATIN CAPITAL LETTER H WITH DOT BELOW › LATIN CAPITAL LETTER H
                case '\u1E26':
                    return '\u0048'; // ? › H LATIN CAPITAL LETTER H WITH DIAERESIS › LATIN CAPITAL LETTER H
                case '\u1E28':
                    return '\u0048'; // ? › H LATIN CAPITAL LETTER H WITH CEDILLA › LATIN CAPITAL LETTER H
                case '\u1E2A':
                    return '\u0048'; // ? › H LATIN CAPITAL LETTER H WITH BREVE BELOW › LATIN CAPITAL LETTER H
                case '\u00CC':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH GRAVE › LATIN CAPITAL LETTER I
                case '\u00CD':
                    return '\u0049'; // Í › I LATIN CAPITAL LETTER I WITH ACUTE › LATIN CAPITAL LETTER I
                case '\u00CE':
                    return '\u0049'; // Î › I LATIN CAPITAL LETTER I WITH CIRCUMFLEX › LATIN CAPITAL LETTER I
                case '\u00CF':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH DIAERESIS › LATIN CAPITAL LETTER I
                case '\u0128':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH TILDE › LATIN CAPITAL LETTER I
                case '\u012A':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH MACRON › LATIN CAPITAL LETTER I
                case '\u012C':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH BREVE › LATIN CAPITAL LETTER I
                case '\u012E':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH OGONEK › LATIN CAPITAL LETTER I
                case '\u0130':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH DOT ABOVE › LATIN CAPITAL LETTER I
                case '\u0197':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH STROKE › LATIN CAPITAL LETTER I
                case '\u01CF':
                    return '\u0049'; // I › I LATIN CAPITAL LETTER I WITH CARON › LATIN CAPITAL LETTER I
                case '\u0208':
                    return '\u0049'; // ? › I LATIN CAPITAL LETTER I WITH DOUBLE GRAVE › LATIN CAPITAL LETTER I
                case '\u020A':
                    return '\u0049'; // ? › I LATIN CAPITAL LETTER I WITH INVERTED BREVE › LATIN CAPITAL LETTER I
                case '\u1E2C':
                    return '\u0049'; // ? › I LATIN CAPITAL LETTER I WITH TILDE BELOW › LATIN CAPITAL LETTER I
                case '\u1E2E':
                    return '\u0049'; // ? › I LATIN CAPITAL LETTER I WITH DIAERESIS AND ACUTE › LATIN CAPITAL LETTER I
                case '\u1EC8':
                    return '\u0049'; // ? › I LATIN CAPITAL LETTER I WITH HOOK ABOVE › LATIN CAPITAL LETTER I
                case '\u1ECA':
                    return '\u0049'; // ? › I LATIN CAPITAL LETTER I WITH DOT BELOW › LATIN CAPITAL LETTER I
                case '\u0134':
                    return '\u004A'; // J › J LATIN CAPITAL LETTER J WITH CIRCUMFLEX › LATIN CAPITAL LETTER J
                case '\u0136':
                    return '\u004B'; // K › K LATIN CAPITAL LETTER K WITH CEDILLA › LATIN CAPITAL LETTER K
                case '\u0198':
                    return '\u004B'; // ? › K LATIN CAPITAL LETTER K WITH HOOK › LATIN CAPITAL LETTER K
                case '\u01E8':
                    return '\u004B'; // K › K LATIN CAPITAL LETTER K WITH CARON › LATIN CAPITAL LETTER K
                case '\u1E30':
                    return '\u004B'; // ? › K LATIN CAPITAL LETTER K WITH ACUTE › LATIN CAPITAL LETTER K
                case '\u1E32':
                    return '\u004B'; // ? › K LATIN CAPITAL LETTER K WITH DOT BELOW › LATIN CAPITAL LETTER K
                case '\u1E34':
                    return '\u004B'; // ? › K LATIN CAPITAL LETTER K WITH LINE BELOW › LATIN CAPITAL LETTER K
                case '\u0139':
                    return '\u004C'; // Ĺ › L LATIN CAPITAL LETTER L WITH ACUTE › LATIN CAPITAL LETTER L
                case '\u013B':
                    return '\u004C'; // L › L LATIN CAPITAL LETTER L WITH CEDILLA › LATIN CAPITAL LETTER L
                case '\u013D':
                    return '\u004C'; // Ľ › L LATIN CAPITAL LETTER L WITH CARON › LATIN CAPITAL LETTER L
                case '\u0141':
                    return '\u004C'; // Ł › L LATIN CAPITAL LETTER L WITH STROKE › LATIN CAPITAL LETTER L
                case '\u1E36':
                    return '\u004C'; // ? › L LATIN CAPITAL LETTER L WITH DOT BELOW › LATIN CAPITAL LETTER L
                case '\u1E38':
                    return '\u004C'; // ? › L LATIN CAPITAL LETTER L WITH DOT BELOW AND MACRON › LATIN CAPITAL LETTER L
                case '\u1E3A':
                    return '\u004C'; // ? › L LATIN CAPITAL LETTER L WITH LINE BELOW › LATIN CAPITAL LETTER L
                case '\u1E3C':
                    return '\u004C'; // ? › L LATIN CAPITAL LETTER L WITH CIRCUMFLEX BELOW › LATIN CAPITAL LETTER L
                case '\u1E3E':
                    return '\u004D'; // ? › M LATIN CAPITAL LETTER M WITH ACUTE › LATIN CAPITAL LETTER M
                case '\u1E40':
                    return '\u004D'; // ? › M LATIN CAPITAL LETTER M WITH DOT ABOVE › LATIN CAPITAL LETTER M
                case '\u1E42':
                    return '\u004D'; // ? › M LATIN CAPITAL LETTER M WITH DOT BELOW › LATIN CAPITAL LETTER M
                case '\u00D1':
                    return '\u004E'; // N › N LATIN CAPITAL LETTER N WITH TILDE › LATIN CAPITAL LETTER N
                case '\u0143':
                    return '\u004E'; // Ń › N LATIN CAPITAL LETTER N WITH ACUTE › LATIN CAPITAL LETTER N
                case '\u0145':
                    return '\u004E'; // N › N LATIN CAPITAL LETTER N WITH CEDILLA › LATIN CAPITAL LETTER N
                case '\u0147':
                    return '\u004E'; // Ň › N LATIN CAPITAL LETTER N WITH CARON › LATIN CAPITAL LETTER N
                case '\u019D':
                    return '\u004E'; // ? › N LATIN CAPITAL LETTER N WITH LEFT HOOK › LATIN CAPITAL LETTER N
                case '\u01F8':
                    return '\u004E'; // ? › N LATIN CAPITAL LETTER N WITH GRAVE › LATIN CAPITAL LETTER N
                case '\u0220':
                    return '\u004E'; // ? › N LATIN CAPITAL LETTER N WITH LONG RIGHT LEG › LATIN CAPITAL LETTER N
                case '\u1E44':
                    return '\u004E'; // ? › N LATIN CAPITAL LETTER N WITH DOT ABOVE › LATIN CAPITAL LETTER N
                case '\u1E46':
                    return '\u004E'; // ? › N LATIN CAPITAL LETTER N WITH DOT BELOW › LATIN CAPITAL LETTER N
                case '\u1E48':
                    return '\u004E'; // ? › N LATIN CAPITAL LETTER N WITH LINE BELOW › LATIN CAPITAL LETTER N
                case '\u1E4A':
                    return '\u004E'; // ? › N LATIN CAPITAL LETTER N WITH CIRCUMFLEX BELOW › LATIN CAPITAL LETTER N
                case '\u00D2':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH GRAVE › LATIN CAPITAL LETTER O
                case '\u00D3':
                    return '\u004F'; // Ó › O LATIN CAPITAL LETTER O WITH ACUTE › LATIN CAPITAL LETTER O
                case '\u00D4':
                    return '\u004F'; // Ô › O LATIN CAPITAL LETTER O WITH CIRCUMFLEX › LATIN CAPITAL LETTER O
                case '\u00D5':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH TILDE › LATIN CAPITAL LETTER O
                case '\u00D6':
                    return '\u004F'; // Ö › O LATIN CAPITAL LETTER O WITH DIAERESIS › LATIN CAPITAL LETTER O
                case '\u00D8':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH STROKE › LATIN CAPITAL LETTER O
                case '\u014C':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH MACRON › LATIN CAPITAL LETTER O
                case '\u014E':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH BREVE › LATIN CAPITAL LETTER O
                case '\u0150':
                    return '\u004F'; // Ő › O LATIN CAPITAL LETTER O WITH DOUBLE ACUTE › LATIN CAPITAL LETTER O
                case '\u019F':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH MIDDLE TILDE › LATIN CAPITAL LETTER O
                case '\u01A0':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH HORN › LATIN CAPITAL LETTER O
                case '\u01D1':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH CARON › LATIN CAPITAL LETTER O
                case '\u01EA':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH OGONEK › LATIN CAPITAL LETTER O
                case '\u01EC':
                    return '\u004F'; // O › O LATIN CAPITAL LETTER O WITH OGONEK AND MACRON › LATIN CAPITAL LETTER O
                case '\u01FE':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH STROKE AND ACUTE › LATIN CAPITAL LETTER O
                case '\u020C':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH DOUBLE GRAVE › LATIN CAPITAL LETTER O
                case '\u020E':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH INVERTED BREVE › LATIN CAPITAL LETTER O
                case '\u022A':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH DIAERESIS AND MACRON › LATIN CAPITAL LETTER O
                case '\u022C':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH TILDE AND MACRON › LATIN CAPITAL LETTER O
                case '\u022E':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH DOT ABOVE › LATIN CAPITAL LETTER O
                case '\u0230':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH DOT ABOVE AND MACRON › LATIN CAPITAL LETTER O
                case '\u1E4C':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH TILDE AND ACUTE › LATIN CAPITAL LETTER O
                case '\u1E4E':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH TILDE AND DIAERESIS › LATIN CAPITAL LETTER O
                case '\u1E50':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH MACRON AND GRAVE › LATIN CAPITAL LETTER O
                case '\u1E52':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH MACRON AND ACUTE › LATIN CAPITAL LETTER O
                case '\u1ECC':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH DOT BELOW › LATIN CAPITAL LETTER O
                case '\u1ECE':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH HOOK ABOVE › LATIN CAPITAL LETTER O
                case '\u1ED0':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND ACUTE › LATIN CAPITAL LETTER O
                case '\u1ED2':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND GRAVE › LATIN CAPITAL LETTER O
                case '\u1ED4':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE › LATIN CAPITAL LETTER O
                case '\u1ED6':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND TILDE › LATIN CAPITAL LETTER O
                case '\u1ED8':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH CIRCUMFLEX AND DOT BELOW › LATIN CAPITAL LETTER O
                case '\u1EDA':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH HORN AND ACUTE › LATIN CAPITAL LETTER O
                case '\u1EDC':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH HORN AND GRAVE › LATIN CAPITAL LETTER O
                case '\u1EDE':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH HORN AND HOOK ABOVE › LATIN CAPITAL LETTER O
                case '\u1EE0':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH HORN AND TILDE › LATIN CAPITAL LETTER O
                case '\u1EE2':
                    return '\u004F'; // ? › O LATIN CAPITAL LETTER O WITH HORN AND DOT BELOW › LATIN CAPITAL LETTER O
                case '\u01A4':
                    return '\u0050'; // ? › P LATIN CAPITAL LETTER P WITH HOOK › LATIN CAPITAL LETTER P
                case '\u1E54':
                    return '\u0050'; // ? › P LATIN CAPITAL LETTER P WITH ACUTE › LATIN CAPITAL LETTER P
                case '\u1E56':
                    return '\u0050'; // ? › P LATIN CAPITAL LETTER P WITH DOT ABOVE › LATIN CAPITAL LETTER P
                case '\u0154':
                    return '\u0052'; // Ŕ › R LATIN CAPITAL LETTER R WITH ACUTE › LATIN CAPITAL LETTER R
                case '\u0156':
                    return '\u0052'; // R › R LATIN CAPITAL LETTER R WITH CEDILLA › LATIN CAPITAL LETTER R
                case '\u0158':
                    return '\u0052'; // Ř › R LATIN CAPITAL LETTER R WITH CARON › LATIN CAPITAL LETTER R
                case '\u0210':
                    return '\u0052'; // ? › R LATIN CAPITAL LETTER R WITH DOUBLE GRAVE › LATIN CAPITAL LETTER R
                case '\u0212':
                    return '\u0052'; // ? › R LATIN CAPITAL LETTER R WITH INVERTED BREVE › LATIN CAPITAL LETTER R
                case '\u1E58':
                    return '\u0052'; // ? › R LATIN CAPITAL LETTER R WITH DOT ABOVE › LATIN CAPITAL LETTER R
                case '\u1E5A':
                    return '\u0052'; // ? › R LATIN CAPITAL LETTER R WITH DOT BELOW › LATIN CAPITAL LETTER R
                case '\u1E5C':
                    return '\u0052'; // ? › R LATIN CAPITAL LETTER R WITH DOT BELOW AND MACRON › LATIN CAPITAL LETTER R
                case '\u1E5E':
                    return '\u0052'; // ? › R LATIN CAPITAL LETTER R WITH LINE BELOW › LATIN CAPITAL LETTER R
                case '\u015A':
                    return '\u0053'; // Ś › S LATIN CAPITAL LETTER S WITH ACUTE › LATIN CAPITAL LETTER S
                case '\u015C':
                    return '\u0053'; // S › S LATIN CAPITAL LETTER S WITH CIRCUMFLEX › LATIN CAPITAL LETTER S
                case '\u015E':
                    return '\u0053'; // Ş › S LATIN CAPITAL LETTER S WITH CEDILLA › LATIN CAPITAL LETTER S
                case '\u0160':
                    return '\u0053'; // Š › S LATIN CAPITAL LETTER S WITH CARON › LATIN CAPITAL LETTER S
                case '\u0218':
                    return '\u0053'; // ? › S LATIN CAPITAL LETTER S WITH COMMA BELOW › LATIN CAPITAL LETTER S
                case '\u1E60':
                    return '\u0053'; // ? › S LATIN CAPITAL LETTER S WITH DOT ABOVE › LATIN CAPITAL LETTER S
                case '\u1E62':
                    return '\u0053'; // ? › S LATIN CAPITAL LETTER S WITH DOT BELOW › LATIN CAPITAL LETTER S
                case '\u1E64':
                    return '\u0053'; // ? › S LATIN CAPITAL LETTER S WITH ACUTE AND DOT ABOVE › LATIN CAPITAL LETTER S
                case '\u1E66':
                    return '\u0053'; // ? › S LATIN CAPITAL LETTER S WITH CARON AND DOT ABOVE › LATIN CAPITAL LETTER S
                case '\u1E68':
                    return '\u0053'; // ? › S LATIN CAPITAL LETTER S WITH DOT BELOW AND DOT ABOVE › LATIN CAPITAL LETTER S
                case '\u0162':
                    return '\u0054'; // Ţ › T LATIN CAPITAL LETTER T WITH CEDILLA › LATIN CAPITAL LETTER T
                case '\u0164':
                    return '\u0054'; // Ť › T LATIN CAPITAL LETTER T WITH CARON › LATIN CAPITAL LETTER T
                case '\u0166':
                    return '\u0054'; // T › T LATIN CAPITAL LETTER T WITH STROKE › LATIN CAPITAL LETTER T
                case '\u01AC':
                    return '\u0054'; // ? › T LATIN CAPITAL LETTER T WITH HOOK › LATIN CAPITAL LETTER T
                case '\u01AE':
                    return '\u0054'; // T › T LATIN CAPITAL LETTER T WITH RETROFLEX HOOK › LATIN CAPITAL LETTER T
                case '\u021A':
                    return '\u0054'; // ? › T LATIN CAPITAL LETTER T WITH COMMA BELOW › LATIN CAPITAL LETTER T
                case '\u1E6A':
                    return '\u0054'; // ? › T LATIN CAPITAL LETTER T WITH DOT ABOVE › LATIN CAPITAL LETTER T
                case '\u1E6C':
                    return '\u0054'; // ? › T LATIN CAPITAL LETTER T WITH DOT BELOW › LATIN CAPITAL LETTER T
                case '\u1E6E':
                    return '\u0054'; // ? › T LATIN CAPITAL LETTER T WITH LINE BELOW › LATIN CAPITAL LETTER T
                case '\u1E70':
                    return '\u0054'; // ? › T LATIN CAPITAL LETTER T WITH CIRCUMFLEX BELOW › LATIN CAPITAL LETTER T
                case '\u00D9':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH GRAVE › LATIN CAPITAL LETTER U
                case '\u00DA':
                    return '\u0055'; // Ú › U LATIN CAPITAL LETTER U WITH ACUTE › LATIN CAPITAL LETTER U
                case '\u00DB':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH CIRCUMFLEX › LATIN CAPITAL LETTER U
                case '\u00DC':
                    return '\u0055'; // Ü › U LATIN CAPITAL LETTER U WITH DIAERESIS › LATIN CAPITAL LETTER U
                case '\u0168':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH TILDE › LATIN CAPITAL LETTER U
                case '\u016A':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH MACRON › LATIN CAPITAL LETTER U
                case '\u016C':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH BREVE › LATIN CAPITAL LETTER U
                case '\u016E':
                    return '\u0055'; // Ů › U LATIN CAPITAL LETTER U WITH RING ABOVE › LATIN CAPITAL LETTER U
                case '\u0170':
                    return '\u0055'; // Ű › U LATIN CAPITAL LETTER U WITH DOUBLE ACUTE › LATIN CAPITAL LETTER U
                case '\u0172':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH OGONEK › LATIN CAPITAL LETTER U
                case '\u01AF':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH HORN › LATIN CAPITAL LETTER U
                case '\u01D3':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH CARON › LATIN CAPITAL LETTER U
                case '\u01D5':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH DIAERESIS AND MACRON › LATIN CAPITAL LETTER U
                case '\u01D7':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH DIAERESIS AND ACUTE › LATIN CAPITAL LETTER U
                case '\u01D9':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH DIAERESIS AND CARON › LATIN CAPITAL LETTER U
                case '\u01DB':
                    return '\u0055'; // U › U LATIN CAPITAL LETTER U WITH DIAERESIS AND GRAVE › LATIN CAPITAL LETTER U
                case '\u0214':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH DOUBLE GRAVE › LATIN CAPITAL LETTER U
                case '\u0216':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH INVERTED BREVE › LATIN CAPITAL LETTER U
                case '\u1E72':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH DIAERESIS BELOW › LATIN CAPITAL LETTER U
                case '\u1E74':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH TILDE BELOW › LATIN CAPITAL LETTER U
                case '\u1E76':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH CIRCUMFLEX BELOW › LATIN CAPITAL LETTER U
                case '\u1E78':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH TILDE AND ACUTE › LATIN CAPITAL LETTER U
                case '\u1E7A':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH MACRON AND DIAERESIS › LATIN CAPITAL LETTER U
                case '\u1EE4':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH DOT BELOW › LATIN CAPITAL LETTER U
                case '\u1EE6':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH HOOK ABOVE › LATIN CAPITAL LETTER U
                case '\u1EE8':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH HORN AND ACUTE › LATIN CAPITAL LETTER U
                case '\u1EEA':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH HORN AND GRAVE › LATIN CAPITAL LETTER U
                case '\u1EEC':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH HORN AND HOOK ABOVE › LATIN CAPITAL LETTER U
                case '\u1EEE':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH HORN AND TILDE › LATIN CAPITAL LETTER U
                case '\u1EF0':
                    return '\u0055'; // ? › U LATIN CAPITAL LETTER U WITH HORN AND DOT BELOW › LATIN CAPITAL LETTER U
                case '\u01B2':
                    return '\u0056'; // ? › V LATIN CAPITAL LETTER V WITH HOOK › LATIN CAPITAL LETTER V
                case '\u1E7C':
                    return '\u0056'; // ? › V LATIN CAPITAL LETTER V WITH TILDE › LATIN CAPITAL LETTER V
                case '\u1E7E':
                    return '\u0056'; // ? › V LATIN CAPITAL LETTER V WITH DOT BELOW › LATIN CAPITAL LETTER V
                case '\u0174':
                    return '\u0057'; // W › W LATIN CAPITAL LETTER W WITH CIRCUMFLEX › LATIN CAPITAL LETTER W
                case '\u1E80':
                    return '\u0057'; // ? › W LATIN CAPITAL LETTER W WITH GRAVE › LATIN CAPITAL LETTER W
                case '\u1E82':
                    return '\u0057'; // ? › W LATIN CAPITAL LETTER W WITH ACUTE › LATIN CAPITAL LETTER W
                case '\u1E84':
                    return '\u0057'; // ? › W LATIN CAPITAL LETTER W WITH DIAERESIS › LATIN CAPITAL LETTER W
                case '\u1E86':
                    return '\u0057'; // ? › W LATIN CAPITAL LETTER W WITH DOT ABOVE › LATIN CAPITAL LETTER W
                case '\u1E88':
                    return '\u0057'; // ? › W LATIN CAPITAL LETTER W WITH DOT BELOW › LATIN CAPITAL LETTER W
                case '\u1E8A':
                    return '\u0058'; // ? › X LATIN CAPITAL LETTER X WITH DOT ABOVE › LATIN CAPITAL LETTER X
                case '\u1E8C':
                    return '\u0058'; // ? › X LATIN CAPITAL LETTER X WITH DIAERESIS › LATIN CAPITAL LETTER X
                case '\u00DD':
                    return '\u0059'; // Ý › Y LATIN CAPITAL LETTER Y WITH ACUTE › LATIN CAPITAL LETTER Y
                case '\u0176':
                    return '\u0059'; // Y › Y LATIN CAPITAL LETTER Y WITH CIRCUMFLEX › LATIN CAPITAL LETTER Y
                case '\u0178':
                    return '\u0059'; // Y › Y LATIN CAPITAL LETTER Y WITH DIAERESIS › LATIN CAPITAL LETTER Y
                case '\u01B3':
                    return '\u0059'; // ? › Y LATIN CAPITAL LETTER Y WITH HOOK › LATIN CAPITAL LETTER Y
                case '\u0232':
                    return '\u0059'; // ? › Y LATIN CAPITAL LETTER Y WITH MACRON › LATIN CAPITAL LETTER Y
                case '\u1E8E':
                    return '\u0059'; // ? › Y LATIN CAPITAL LETTER Y WITH DOT ABOVE › LATIN CAPITAL LETTER Y
                case '\u1EF2':
                    return '\u0059'; // ? › Y LATIN CAPITAL LETTER Y WITH GRAVE › LATIN CAPITAL LETTER Y
                case '\u1EF4':
                    return '\u0059'; // ? › Y LATIN CAPITAL LETTER Y WITH DOT BELOW › LATIN CAPITAL LETTER Y
                case '\u1EF6':
                    return '\u0059'; // ? › Y LATIN CAPITAL LETTER Y WITH HOOK ABOVE › LATIN CAPITAL LETTER Y
                case '\u1EF8':
                    return '\u0059'; // ? › Y LATIN CAPITAL LETTER Y WITH TILDE › LATIN CAPITAL LETTER Y
                case '\u0179':
                    return '\u005A'; // Ź › Z LATIN CAPITAL LETTER Z WITH ACUTE › LATIN CAPITAL LETTER Z
                case '\u017B':
                    return '\u005A'; // Ż › Z LATIN CAPITAL LETTER Z WITH DOT ABOVE › LATIN CAPITAL LETTER Z
                case '\u017D':
                    return '\u005A'; // Ž › Z LATIN CAPITAL LETTER Z WITH CARON › LATIN CAPITAL LETTER Z
                case '\u01B5':
                    return '\u005A'; // ? › Z LATIN CAPITAL LETTER Z WITH STROKE › LATIN CAPITAL LETTER Z
                case '\u0224':
                    return '\u005A'; // ? › Z LATIN CAPITAL LETTER Z WITH HOOK › LATIN CAPITAL LETTER Z
                case '\u1E90':
                    return '\u005A'; // ? › Z LATIN CAPITAL LETTER Z WITH CIRCUMFLEX › LATIN CAPITAL LETTER Z
                case '\u1E92':
                    return '\u005A'; // ? › Z LATIN CAPITAL LETTER Z WITH DOT BELOW › LATIN CAPITAL LETTER Z
                case '\u1E94':
                    return '\u005A'; // ? › Z LATIN CAPITAL LETTER Z WITH LINE BELOW › LATIN CAPITAL LETTER Z
                case '\u00E0':
                    return '\u0061'; // a › a LATIN SMALL LETTER A WITH GRAVE › LATIN SMALL LETTER A
                case '\u00E1':
                    return '\u0061'; // á › a LATIN SMALL LETTER A WITH ACUTE › LATIN SMALL LETTER A
                case '\u00E2':
                    return '\u0061'; // â › a LATIN SMALL LETTER A WITH CIRCUMFLEX › LATIN SMALL LETTER A
                case '\u00E3':
                    return '\u0061'; // a › a LATIN SMALL LETTER A WITH TILDE › LATIN SMALL LETTER A
                case '\u00E4':
                    return '\u0061'; // ä › a LATIN SMALL LETTER A WITH DIAERESIS › LATIN SMALL LETTER A
                case '\u00E5':
                    return '\u0061'; // a › a LATIN SMALL LETTER A WITH RING ABOVE › LATIN SMALL LETTER A
                case '\u0101':
                    return '\u0061'; // a › a LATIN SMALL LETTER A WITH MACRON › LATIN SMALL LETTER A
                case '\u0103':
                    return '\u0061'; // ă › a LATIN SMALL LETTER A WITH BREVE › LATIN SMALL LETTER A
                case '\u0105':
                    return '\u0061'; // ą › a LATIN SMALL LETTER A WITH OGONEK › LATIN SMALL LETTER A
                case '\u01CE':
                    return '\u0061'; // a › a LATIN SMALL LETTER A WITH CARON › LATIN SMALL LETTER A
                case '\u01DF':
                    return '\u0061'; // a › a LATIN SMALL LETTER A WITH DIAERESIS AND MACRON › LATIN SMALL LETTER A
                case '\u01E1':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH DOT ABOVE AND MACRON › LATIN SMALL LETTER A
                case '\u01FB':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH RING ABOVE AND ACUTE › LATIN SMALL LETTER A
                case '\u0201':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH DOUBLE GRAVE › LATIN SMALL LETTER A
                case '\u0203':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH INVERTED BREVE › LATIN SMALL LETTER A
                case '\u0227':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH DOT ABOVE › LATIN SMALL LETTER A
                case '\u1E01':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH RING BELOW › LATIN SMALL LETTER A
                case '\u1E9A':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH RIGHT HALF RING › LATIN SMALL LETTER A
                case '\u1EA1':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH DOT BELOW › LATIN SMALL LETTER A
                case '\u1EA3':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH HOOK ABOVE › LATIN SMALL LETTER A
                case '\u1EA5':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH CIRCUMFLEX AND ACUTE › LATIN SMALL LETTER A
                case '\u1EA7':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH CIRCUMFLEX AND GRAVE › LATIN SMALL LETTER A
                case '\u1EA9':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH CIRCUMFLEX AND HOOK ABOVE › LATIN SMALL LETTER A
                case '\u1EAB':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH CIRCUMFLEX AND TILDE › LATIN SMALL LETTER A
                case '\u1EAD':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH CIRCUMFLEX AND DOT BELOW › LATIN SMALL LETTER A
                case '\u1EAF':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH BREVE AND ACUTE › LATIN SMALL LETTER A
                case '\u1EB1':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH BREVE AND GRAVE › LATIN SMALL LETTER A
                case '\u1EB3':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH BREVE AND HOOK ABOVE › LATIN SMALL LETTER A
                case '\u1EB5':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH BREVE AND TILDE › LATIN SMALL LETTER A
                case '\u1EB7':
                    return '\u0061'; // ? › a LATIN SMALL LETTER A WITH BREVE AND DOT BELOW › LATIN SMALL LETTER A
                case '\u0180':
                    return '\u0062'; // b › b LATIN SMALL LETTER B WITH STROKE › LATIN SMALL LETTER B
                case '\u0183':
                    return '\u0062'; // ? › b LATIN SMALL LETTER B WITH TOPBAR › LATIN SMALL LETTER B
                case '\u0253':
                    return '\u0062'; // ? › b LATIN SMALL LETTER B WITH HOOK › LATIN SMALL LETTER B
                case '\u1E03':
                    return '\u0062'; // ? › b LATIN SMALL LETTER B WITH DOT ABOVE › LATIN SMALL LETTER B
                case '\u1E05':
                    return '\u0062'; // ? › b LATIN SMALL LETTER B WITH DOT BELOW › LATIN SMALL LETTER B
                case '\u1E07':
                    return '\u0062'; // ? › b LATIN SMALL LETTER B WITH LINE BELOW › LATIN SMALL LETTER B
                case '\u00E7':
                    return '\u0063'; // ç › c LATIN SMALL LETTER C WITH CEDILLA › LATIN SMALL LETTER C
                case '\u0107':
                    return '\u0063'; // ć › c LATIN SMALL LETTER C WITH ACUTE › LATIN SMALL LETTER C
                case '\u0109':
                    return '\u0063'; // c › c LATIN SMALL LETTER C WITH CIRCUMFLEX › LATIN SMALL LETTER C
                case '\u010B':
                    return '\u0063'; // c › c LATIN SMALL LETTER C WITH DOT ABOVE › LATIN SMALL LETTER C
                case '\u010D':
                    return '\u0063'; // č › c LATIN SMALL LETTER C WITH CARON › LATIN SMALL LETTER C
                case '\u0188':
                    return '\u0063'; // ? › c LATIN SMALL LETTER C WITH HOOK › LATIN SMALL LETTER C
                case '\u0255':
                    return '\u0063'; // ? › c LATIN SMALL LETTER C WITH CURL › LATIN SMALL LETTER C
                case '\u1E09':
                    return '\u0063'; // ? › c LATIN SMALL LETTER C WITH CEDILLA AND ACUTE › LATIN SMALL LETTER C
                case '\u010F':
                    return '\u0064'; // ď › d LATIN SMALL LETTER D WITH CARON › LATIN SMALL LETTER D
                case '\u0111':
                    return '\u0064'; // đ › d LATIN SMALL LETTER D WITH STROKE › LATIN SMALL LETTER D
                case '\u018C':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH TOPBAR › LATIN SMALL LETTER D
                case '\u0221':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH CURL › LATIN SMALL LETTER D
                case '\u0256':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH TAIL › LATIN SMALL LETTER D
                case '\u0257':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH HOOK › LATIN SMALL LETTER D
                case '\u1E0B':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH DOT ABOVE › LATIN SMALL LETTER D
                case '\u1E0D':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH DOT BELOW › LATIN SMALL LETTER D
                case '\u1E0F':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH LINE BELOW › LATIN SMALL LETTER D
                case '\u1E11':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH CEDILLA › LATIN SMALL LETTER D
                case '\u1E13':
                    return '\u0064'; // ? › d LATIN SMALL LETTER D WITH CIRCUMFLEX BELOW › LATIN SMALL LETTER D
                case '\u00E8':
                    return '\u0065'; // e › e LATIN SMALL LETTER E WITH GRAVE › LATIN SMALL LETTER E
                case '\u00E9':
                    return '\u0065'; // é › e LATIN SMALL LETTER E WITH ACUTE › LATIN SMALL LETTER E
                case '\u00EA':
                    return '\u0065'; // e › e LATIN SMALL LETTER E WITH CIRCUMFLEX › LATIN SMALL LETTER E
                case '\u00EB':
                    return '\u0065'; // ë › e LATIN SMALL LETTER E WITH DIAERESIS › LATIN SMALL LETTER E
                case '\u0113':
                    return '\u0065'; // e › e LATIN SMALL LETTER E WITH MACRON › LATIN SMALL LETTER E
                case '\u0115':
                    return '\u0065'; // e › e LATIN SMALL LETTER E WITH BREVE › LATIN SMALL LETTER E
                case '\u0117':
                    return '\u0065'; // e › e LATIN SMALL LETTER E WITH DOT ABOVE › LATIN SMALL LETTER E
                case '\u0119':
                    return '\u0065'; // ę › e LATIN SMALL LETTER E WITH OGONEK › LATIN SMALL LETTER E
                case '\u011B':
                    return '\u0065'; // ě › e LATIN SMALL LETTER E WITH CARON › LATIN SMALL LETTER E
                case '\u0205':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH DOUBLE GRAVE › LATIN SMALL LETTER E
                case '\u0207':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH INVERTED BREVE › LATIN SMALL LETTER E
                case '\u0229':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CEDILLA › LATIN SMALL LETTER E
                case '\u1E15':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH MACRON AND GRAVE › LATIN SMALL LETTER E
                case '\u1E17':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH MACRON AND ACUTE › LATIN SMALL LETTER E
                case '\u1E19':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CIRCUMFLEX BELOW › LATIN SMALL LETTER E
                case '\u1E1B':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH TILDE BELOW › LATIN SMALL LETTER E
                case '\u1E1D':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CEDILLA AND BREVE › LATIN SMALL LETTER E
                case '\u1EB9':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH DOT BELOW › LATIN SMALL LETTER E
                case '\u1EBB':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH HOOK ABOVE › LATIN SMALL LETTER E
                case '\u1EBD':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH TILDE › LATIN SMALL LETTER E
                case '\u1EBF':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CIRCUMFLEX AND ACUTE › LATIN SMALL LETTER E
                case '\u1EC1':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CIRCUMFLEX AND GRAVE › LATIN SMALL LETTER E
                case '\u1EC3':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CIRCUMFLEX AND HOOK ABOVE › LATIN SMALL LETTER E
                case '\u1EC5':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CIRCUMFLEX AND TILDE › LATIN SMALL LETTER E
                case '\u1EC7':
                    return '\u0065'; // ? › e LATIN SMALL LETTER E WITH CIRCUMFLEX AND DOT BELOW › LATIN SMALL LETTER E
                case '\u0192':
                    return '\u0066'; // f › f LATIN SMALL LETTER F WITH HOOK › LATIN SMALL LETTER F
                case '\u1E1F':
                    return '\u0066'; // ? › f LATIN SMALL LETTER F WITH DOT ABOVE › LATIN SMALL LETTER F
                case '\u011D':
                    return '\u0067'; // g › g LATIN SMALL LETTER G WITH CIRCUMFLEX › LATIN SMALL LETTER G
                case '\u011F':
                    return '\u0067'; // g › g LATIN SMALL LETTER G WITH BREVE › LATIN SMALL LETTER G
                case '\u0121':
                    return '\u0067'; // g › g LATIN SMALL LETTER G WITH DOT ABOVE › LATIN SMALL LETTER G
                case '\u0123':
                    return '\u0067'; // g › g LATIN SMALL LETTER G WITH CEDILLA › LATIN SMALL LETTER G
                case '\u01E5':
                    return '\u0067'; // g › g LATIN SMALL LETTER G WITH STROKE › LATIN SMALL LETTER G
                case '\u01E7':
                    return '\u0067'; // g › g LATIN SMALL LETTER G WITH CARON › LATIN SMALL LETTER G
                case '\u01F5':
                    return '\u0067'; // ? › g LATIN SMALL LETTER G WITH ACUTE › LATIN SMALL LETTER G
                case '\u0260':
                    return '\u0067'; // ? › g LATIN SMALL LETTER G WITH HOOK › LATIN SMALL LETTER G
                case '\u1E21':
                    return '\u0067'; // ? › g LATIN SMALL LETTER G WITH MACRON › LATIN SMALL LETTER G
                case '\u0125':
                    return '\u0068'; // h › h LATIN SMALL LETTER H WITH CIRCUMFLEX › LATIN SMALL LETTER H
                case '\u0127':
                    return '\u0068'; // h › h LATIN SMALL LETTER H WITH STROKE › LATIN SMALL LETTER H
                case '\u021F':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH CARON › LATIN SMALL LETTER H
                case '\u0266':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH HOOK › LATIN SMALL LETTER H
                case '\u1E23':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH DOT ABOVE › LATIN SMALL LETTER H
                case '\u1E25':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH DOT BELOW › LATIN SMALL LETTER H
                case '\u1E27':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH DIAERESIS › LATIN SMALL LETTER H
                case '\u1E29':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH CEDILLA › LATIN SMALL LETTER H
                case '\u1E2B':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH BREVE BELOW › LATIN SMALL LETTER H
                case '\u1E96':
                    return '\u0068'; // ? › h LATIN SMALL LETTER H WITH LINE BELOW › LATIN SMALL LETTER H
                case '\u00EC':
                    return '\u0069'; // i › i LATIN SMALL LETTER I WITH GRAVE › LATIN SMALL LETTER I
                case '\u00ED':
                    return '\u0069'; // í › i LATIN SMALL LETTER I WITH ACUTE › LATIN SMALL LETTER I
                case '\u00EE':
                    return '\u0069'; // î › i LATIN SMALL LETTER I WITH CIRCUMFLEX › LATIN SMALL LETTER I
                case '\u00EF':
                    return '\u0069'; // i › i LATIN SMALL LETTER I WITH DIAERESIS › LATIN SMALL LETTER I
                case '\u0129':
                    return '\u0069'; // i › i LATIN SMALL LETTER I WITH TILDE › LATIN SMALL LETTER I
                case '\u012B':
                    return '\u0069'; // i › i LATIN SMALL LETTER I WITH MACRON › LATIN SMALL LETTER I
                case '\u012D':
                    return '\u0069'; // i › i LATIN SMALL LETTER I WITH BREVE › LATIN SMALL LETTER I
                case '\u012F':
                    return '\u0069'; // i › i LATIN SMALL LETTER I WITH OGONEK › LATIN SMALL LETTER I
                case '\u01D0':
                    return '\u0069'; // i › i LATIN SMALL LETTER I WITH CARON › LATIN SMALL LETTER I
                case '\u0209':
                    return '\u0069'; // ? › i LATIN SMALL LETTER I WITH DOUBLE GRAVE › LATIN SMALL LETTER I
                case '\u020B':
                    return '\u0069'; // ? › i LATIN SMALL LETTER I WITH INVERTED BREVE › LATIN SMALL LETTER I
                case '\u0268':
                    return '\u0069'; // ? › i LATIN SMALL LETTER I WITH STROKE › LATIN SMALL LETTER I
                case '\u1E2D':
                    return '\u0069'; // ? › i LATIN SMALL LETTER I WITH TILDE BELOW › LATIN SMALL LETTER I
                case '\u1E2F':
                    return '\u0069'; // ? › i LATIN SMALL LETTER I WITH DIAERESIS AND ACUTE › LATIN SMALL LETTER I
                case '\u1EC9':
                    return '\u0069'; // ? › i LATIN SMALL LETTER I WITH HOOK ABOVE › LATIN SMALL LETTER I
                case '\u1ECB':
                    return '\u0069'; // ? › i LATIN SMALL LETTER I WITH DOT BELOW › LATIN SMALL LETTER I
                case '\u0135':
                    return '\u006A'; // j › j LATIN SMALL LETTER J WITH CIRCUMFLEX › LATIN SMALL LETTER J
                case '\u01F0':
                    return '\u006A'; // j › j LATIN SMALL LETTER J WITH CARON › LATIN SMALL LETTER J
                case '\u029D':
                    return '\u006A'; // ? › j LATIN SMALL LETTER J WITH CROSSED-TAIL › LATIN SMALL LETTER J
                case '\u0137':
                    return '\u006B'; // k › k LATIN SMALL LETTER K WITH CEDILLA › LATIN SMALL LETTER K
                case '\u0199':
                    return '\u006B'; // ? › k LATIN SMALL LETTER K WITH HOOK › LATIN SMALL LETTER K
                case '\u01E9':
                    return '\u006B'; // k › k LATIN SMALL LETTER K WITH CARON › LATIN SMALL LETTER K
                case '\u1E31':
                    return '\u006B'; // ? › k LATIN SMALL LETTER K WITH ACUTE › LATIN SMALL LETTER K
                case '\u1E33':
                    return '\u006B'; // ? › k LATIN SMALL LETTER K WITH DOT BELOW › LATIN SMALL LETTER K
                case '\u1E35':
                    return '\u006B'; // ? › k LATIN SMALL LETTER K WITH LINE BELOW › LATIN SMALL LETTER K
                case '\u013A':
                    return '\u006C'; // ĺ › l LATIN SMALL LETTER L WITH ACUTE › LATIN SMALL LETTER L
                case '\u013C':
                    return '\u006C'; // l › l LATIN SMALL LETTER L WITH CEDILLA › LATIN SMALL LETTER L
                case '\u013E':
                    return '\u006C'; // ľ › l LATIN SMALL LETTER L WITH CARON › LATIN SMALL LETTER L
                case '\u0140':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH MIDDLE DOT › LATIN SMALL LETTER L
                case '\u0142':
                    return '\u006C'; // ł › l LATIN SMALL LETTER L WITH STROKE › LATIN SMALL LETTER L
                case '\u019A':
                    return '\u006C'; // l › l LATIN SMALL LETTER L WITH BAR › LATIN SMALL LETTER L
                case '\u0234':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH CURL › LATIN SMALL LETTER L
                case '\u026B':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH MIDDLE TILDE › LATIN SMALL LETTER L
                case '\u026C':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH BELT › LATIN SMALL LETTER L
                case '\u026D':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH RETROFLEX HOOK › LATIN SMALL LETTER L
                case '\u1E37':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH DOT BELOW › LATIN SMALL LETTER L
                case '\u1E39':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH DOT BELOW AND MACRON › LATIN SMALL LETTER L
                case '\u1E3B':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH LINE BELOW › LATIN SMALL LETTER L
                case '\u1E3D':
                    return '\u006C'; // ? › l LATIN SMALL LETTER L WITH CIRCUMFLEX BELOW › LATIN SMALL LETTER L
                case '\u0271':
                    return '\u006D'; // ? › m LATIN SMALL LETTER M WITH HOOK › LATIN SMALL LETTER M
                case '\u1E3F':
                    return '\u006D'; // ? › m LATIN SMALL LETTER M WITH ACUTE › LATIN SMALL LETTER M
                case '\u1E41':
                    return '\u006D'; // ? › m LATIN SMALL LETTER M WITH DOT ABOVE › LATIN SMALL LETTER M
                case '\u1E43':
                    return '\u006D'; // ? › m LATIN SMALL LETTER M WITH DOT BELOW › LATIN SMALL LETTER M
                case '\u00F1':
                    return '\u006E'; // n › n LATIN SMALL LETTER N WITH TILDE › LATIN SMALL LETTER N
                case '\u0144':
                    return '\u006E'; // ń › n LATIN SMALL LETTER N WITH ACUTE › LATIN SMALL LETTER N
                case '\u0146':
                    return '\u006E'; // n › n LATIN SMALL LETTER N WITH CEDILLA › LATIN SMALL LETTER N
                case '\u0148':
                    return '\u006E'; // ň › n LATIN SMALL LETTER N WITH CARON › LATIN SMALL LETTER N
                case '\u019E':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH LONG RIGHT LEG › LATIN SMALL LETTER N
                case '\u01F9':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH GRAVE › LATIN SMALL LETTER N
                case '\u0235':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH CURL › LATIN SMALL LETTER N
                case '\u0272':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH LEFT HOOK › LATIN SMALL LETTER N
                case '\u0273':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH RETROFLEX HOOK › LATIN SMALL LETTER N
                case '\u1E45':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH DOT ABOVE › LATIN SMALL LETTER N
                case '\u1E47':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH DOT BELOW › LATIN SMALL LETTER N
                case '\u1E49':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH LINE BELOW › LATIN SMALL LETTER N
                case '\u1E4B':
                    return '\u006E'; // ? › n LATIN SMALL LETTER N WITH CIRCUMFLEX BELOW › LATIN SMALL LETTER N
                case '\u00F2':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH GRAVE › LATIN SMALL LETTER O
                case '\u00F3':
                    return '\u006F'; // ó › o LATIN SMALL LETTER O WITH ACUTE › LATIN SMALL LETTER O
                case '\u00F4':
                    return '\u006F'; // ô › o LATIN SMALL LETTER O WITH CIRCUMFLEX › LATIN SMALL LETTER O
                case '\u00F5':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH TILDE › LATIN SMALL LETTER O
                case '\u00F6':
                    return '\u006F'; // ö › o LATIN SMALL LETTER O WITH DIAERESIS › LATIN SMALL LETTER O
                case '\u00F8':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH STROKE › LATIN SMALL LETTER O
                case '\u014D':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH MACRON › LATIN SMALL LETTER O
                case '\u014F':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH BREVE › LATIN SMALL LETTER O
                case '\u0151':
                    return '\u006F'; // ő › o LATIN SMALL LETTER O WITH DOUBLE ACUTE › LATIN SMALL LETTER O
                case '\u01A1':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH HORN › LATIN SMALL LETTER O
                case '\u01D2':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH CARON › LATIN SMALL LETTER O
                case '\u01EB':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH OGONEK › LATIN SMALL LETTER O
                case '\u01ED':
                    return '\u006F'; // o › o LATIN SMALL LETTER O WITH OGONEK AND MACRON › LATIN SMALL LETTER O
                case '\u01FF':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH STROKE AND ACUTE › LATIN SMALL LETTER O
                case '\u020D':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH DOUBLE GRAVE › LATIN SMALL LETTER O
                case '\u020F':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH INVERTED BREVE › LATIN SMALL LETTER O
                case '\u022B':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH DIAERESIS AND MACRON › LATIN SMALL LETTER O
                case '\u022D':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH TILDE AND MACRON › LATIN SMALL LETTER O
                case '\u022F':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH DOT ABOVE › LATIN SMALL LETTER O
                case '\u0231':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH DOT ABOVE AND MACRON › LATIN SMALL LETTER O
                case '\u1E4D':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH TILDE AND ACUTE › LATIN SMALL LETTER O
                case '\u1E4F':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH TILDE AND DIAERESIS › LATIN SMALL LETTER O
                case '\u1E51':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH MACRON AND GRAVE › LATIN SMALL LETTER O
                case '\u1E53':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH MACRON AND ACUTE › LATIN SMALL LETTER O
                case '\u1ECD':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH DOT BELOW › LATIN SMALL LETTER O
                case '\u1ECF':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH HOOK ABOVE › LATIN SMALL LETTER O
                case '\u1ED1':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH CIRCUMFLEX AND ACUTE › LATIN SMALL LETTER O
                case '\u1ED3':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH CIRCUMFLEX AND GRAVE › LATIN SMALL LETTER O
                case '\u1ED5':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH CIRCUMFLEX AND HOOK ABOVE › LATIN SMALL LETTER O
                case '\u1ED7':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH CIRCUMFLEX AND TILDE › LATIN SMALL LETTER O
                case '\u1ED9':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH CIRCUMFLEX AND DOT BELOW › LATIN SMALL LETTER O
                case '\u1EDB':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH HORN AND ACUTE › LATIN SMALL LETTER O
                case '\u1EDD':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH HORN AND GRAVE › LATIN SMALL LETTER O
                case '\u1EDF':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH HORN AND HOOK ABOVE › LATIN SMALL LETTER O
                case '\u1EE1':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH HORN AND TILDE › LATIN SMALL LETTER O
                case '\u1EE3':
                    return '\u006F'; // ? › o LATIN SMALL LETTER O WITH HORN AND DOT BELOW › LATIN SMALL LETTER O
                case '\u01A5':
                    return '\u0070'; // ? › p LATIN SMALL LETTER P WITH HOOK › LATIN SMALL LETTER P
                case '\u1E55':
                    return '\u0070'; // ? › p LATIN SMALL LETTER P WITH ACUTE › LATIN SMALL LETTER P
                case '\u1E57':
                    return '\u0070'; // ? › p LATIN SMALL LETTER P WITH DOT ABOVE › LATIN SMALL LETTER P
                case '\u02A0':
                    return '\u0071'; // ? › q LATIN SMALL LETTER Q WITH HOOK › LATIN SMALL LETTER Q
                case '\u0155':
                    return '\u0072'; // ŕ › r LATIN SMALL LETTER R WITH ACUTE › LATIN SMALL LETTER R
                case '\u0157':
                    return '\u0072'; // r › r LATIN SMALL LETTER R WITH CEDILLA › LATIN SMALL LETTER R
                case '\u0159':
                    return '\u0072'; // ř › r LATIN SMALL LETTER R WITH CARON › LATIN SMALL LETTER R
                case '\u0211':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH DOUBLE GRAVE › LATIN SMALL LETTER R
                case '\u0213':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH INVERTED BREVE › LATIN SMALL LETTER R
                case '\u027C':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH LONG LEG › LATIN SMALL LETTER R
                case '\u027D':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH TAIL › LATIN SMALL LETTER R
                case '\u1E59':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH DOT ABOVE › LATIN SMALL LETTER R
                case '\u1E5B':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH DOT BELOW › LATIN SMALL LETTER R
                case '\u1E5D':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH DOT BELOW AND MACRON › LATIN SMALL LETTER R
                case '\u1E5F':
                    return '\u0072'; // ? › r LATIN SMALL LETTER R WITH LINE BELOW › LATIN SMALL LETTER R
                case '\u015B':
                    return '\u0073'; // ś › s LATIN SMALL LETTER S WITH ACUTE › LATIN SMALL LETTER S
                case '\u015D':
                    return '\u0073'; // s › s LATIN SMALL LETTER S WITH CIRCUMFLEX › LATIN SMALL LETTER S
                case '\u015F':
                    return '\u0073'; // ş › s LATIN SMALL LETTER S WITH CEDILLA › LATIN SMALL LETTER S
                case '\u0161':
                    return '\u0073'; // š › s LATIN SMALL LETTER S WITH CARON › LATIN SMALL LETTER S
                case '\u0219':
                    return '\u0073'; // ? › s LATIN SMALL LETTER S WITH COMMA BELOW › LATIN SMALL LETTER S
                case '\u0282':
                    return '\u0073'; // ? › s LATIN SMALL LETTER S WITH HOOK › LATIN SMALL LETTER S
                case '\u1E61':
                    return '\u0073'; // ? › s LATIN SMALL LETTER S WITH DOT ABOVE › LATIN SMALL LETTER S
                case '\u1E63':
                    return '\u0073'; // ? › s LATIN SMALL LETTER S WITH DOT BELOW › LATIN SMALL LETTER S
                case '\u1E65':
                    return '\u0073'; // ? › s LATIN SMALL LETTER S WITH ACUTE AND DOT ABOVE › LATIN SMALL LETTER S
                case '\u1E67':
                    return '\u0073'; // ? › s LATIN SMALL LETTER S WITH CARON AND DOT ABOVE › LATIN SMALL LETTER S
                case '\u1E69':
                    return '\u0073'; // ? › s LATIN SMALL LETTER S WITH DOT BELOW AND DOT ABOVE › LATIN SMALL LETTER S
                case '\u0163':
                    return '\u0074'; // ţ › t LATIN SMALL LETTER T WITH CEDILLA › LATIN SMALL LETTER T
                case '\u0165':
                    return '\u0074'; // ť › t LATIN SMALL LETTER T WITH CARON › LATIN SMALL LETTER T
                case '\u0167':
                    return '\u0074'; // t › t LATIN SMALL LETTER T WITH STROKE › LATIN SMALL LETTER T
                case '\u01AB':
                    return '\u0074'; // t › t LATIN SMALL LETTER T WITH PALATAL HOOK › LATIN SMALL LETTER T
                case '\u01AD':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH HOOK › LATIN SMALL LETTER T
                case '\u021B':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH COMMA BELOW › LATIN SMALL LETTER T
                case '\u0236':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH CURL › LATIN SMALL LETTER T
                case '\u0288':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH RETROFLEX HOOK › LATIN SMALL LETTER T
                case '\u1E6B':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH DOT ABOVE › LATIN SMALL LETTER T
                case '\u1E6D':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH DOT BELOW › LATIN SMALL LETTER T
                case '\u1E6F':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH LINE BELOW › LATIN SMALL LETTER T
                case '\u1E71':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH CIRCUMFLEX BELOW › LATIN SMALL LETTER T
                case '\u1E97':
                    return '\u0074'; // ? › t LATIN SMALL LETTER T WITH DIAERESIS › LATIN SMALL LETTER T
                case '\u00F9':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH GRAVE › LATIN SMALL LETTER U
                case '\u00FA':
                    return '\u0075'; // ú › u LATIN SMALL LETTER U WITH ACUTE › LATIN SMALL LETTER U
                case '\u00FB':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH CIRCUMFLEX › LATIN SMALL LETTER U
                case '\u00FC':
                    return '\u0075'; // ü › u LATIN SMALL LETTER U WITH DIAERESIS › LATIN SMALL LETTER U
                case '\u0169':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH TILDE › LATIN SMALL LETTER U
                case '\u016B':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH MACRON › LATIN SMALL LETTER U
                case '\u016D':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH BREVE › LATIN SMALL LETTER U
                case '\u016F':
                    return '\u0075'; // ů › u LATIN SMALL LETTER U WITH RING ABOVE › LATIN SMALL LETTER U
                case '\u0171':
                    return '\u0075'; // ű › u LATIN SMALL LETTER U WITH DOUBLE ACUTE › LATIN SMALL LETTER U
                case '\u0173':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH OGONEK › LATIN SMALL LETTER U
                case '\u01B0':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH HORN › LATIN SMALL LETTER U
                case '\u01D4':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH CARON › LATIN SMALL LETTER U
                case '\u01D6':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH DIAERESIS AND MACRON › LATIN SMALL LETTER U
                case '\u01D8':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH DIAERESIS AND ACUTE › LATIN SMALL LETTER U
                case '\u01DA':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH DIAERESIS AND CARON › LATIN SMALL LETTER U
                case '\u01DC':
                    return '\u0075'; // u › u LATIN SMALL LETTER U WITH DIAERESIS AND GRAVE › LATIN SMALL LETTER U
                case '\u0215':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH DOUBLE GRAVE › LATIN SMALL LETTER U
                case '\u0217':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH INVERTED BREVE › LATIN SMALL LETTER U
                case '\u1E73':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH DIAERESIS BELOW › LATIN SMALL LETTER U
                case '\u1E75':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH TILDE BELOW › LATIN SMALL LETTER U
                case '\u1E77':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH CIRCUMFLEX BELOW › LATIN SMALL LETTER U
                case '\u1E79':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH TILDE AND ACUTE › LATIN SMALL LETTER U
                case '\u1E7B':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH MACRON AND DIAERESIS › LATIN SMALL LETTER U
                case '\u1EE5':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH DOT BELOW › LATIN SMALL LETTER U
                case '\u1EE7':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH HOOK ABOVE › LATIN SMALL LETTER U
                case '\u1EE9':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH HORN AND ACUTE › LATIN SMALL LETTER U
                case '\u1EEB':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH HORN AND GRAVE › LATIN SMALL LETTER U
                case '\u1EED':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH HORN AND HOOK ABOVE › LATIN SMALL LETTER U
                case '\u1EEF':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH HORN AND TILDE › LATIN SMALL LETTER U
                case '\u1EF1':
                    return '\u0075'; // ? › u LATIN SMALL LETTER U WITH HORN AND DOT BELOW › LATIN SMALL LETTER U
                case '\u028B':
                    return '\u0076'; // ? › v LATIN SMALL LETTER V WITH HOOK › LATIN SMALL LETTER V
                case '\u1E7D':
                    return '\u0076'; // ? › v LATIN SMALL LETTER V WITH TILDE › LATIN SMALL LETTER V
                case '\u1E7F':
                    return '\u0076'; // ? › v LATIN SMALL LETTER V WITH DOT BELOW › LATIN SMALL LETTER V
                case '\u0175':
                    return '\u0077'; // w › w LATIN SMALL LETTER W WITH CIRCUMFLEX › LATIN SMALL LETTER W
                case '\u1E81':
                    return '\u0077'; // ? › w LATIN SMALL LETTER W WITH GRAVE › LATIN SMALL LETTER W
                case '\u1E83':
                    return '\u0077'; // ? › w LATIN SMALL LETTER W WITH ACUTE › LATIN SMALL LETTER W
                case '\u1E85':
                    return '\u0077'; // ? › w LATIN SMALL LETTER W WITH DIAERESIS › LATIN SMALL LETTER W
                case '\u1E87':
                    return '\u0077'; // ? › w LATIN SMALL LETTER W WITH DOT ABOVE › LATIN SMALL LETTER W
                case '\u1E89':
                    return '\u0077'; // ? › w LATIN SMALL LETTER W WITH DOT BELOW › LATIN SMALL LETTER W
                case '\u1E98':
                    return '\u0077'; // ? › w LATIN SMALL LETTER W WITH RING ABOVE › LATIN SMALL LETTER W
                case '\u1E8B':
                    return '\u0078'; // ? › x LATIN SMALL LETTER X WITH DOT ABOVE › LATIN SMALL LETTER X
                case '\u1E8D':
                    return '\u0078'; // ? › x LATIN SMALL LETTER X WITH DIAERESIS › LATIN SMALL LETTER X
                case '\u00FD':
                    return '\u0079'; // ý › y LATIN SMALL LETTER Y WITH ACUTE › LATIN SMALL LETTER Y
                case '\u00FF':
                    return '\u0079'; // y › y LATIN SMALL LETTER Y WITH DIAERESIS › LATIN SMALL LETTER Y
                case '\u0177':
                    return '\u0079'; // y › y LATIN SMALL LETTER Y WITH CIRCUMFLEX › LATIN SMALL LETTER Y
                case '\u01B4':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH HOOK › LATIN SMALL LETTER Y
                case '\u0233':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH MACRON › LATIN SMALL LETTER Y
                case '\u1E8F':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH DOT ABOVE › LATIN SMALL LETTER Y
                case '\u1E99':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH RING ABOVE › LATIN SMALL LETTER Y
                case '\u1EF3':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH GRAVE › LATIN SMALL LETTER Y
                case '\u1EF5':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH DOT BELOW › LATIN SMALL LETTER Y
                case '\u1EF7':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH HOOK ABOVE › LATIN SMALL LETTER Y
                case '\u1EF9':
                    return '\u0079'; // ? › y LATIN SMALL LETTER Y WITH TILDE › LATIN SMALL LETTER Y
                case '\u017A':
                    return '\u007A'; // ź › z LATIN SMALL LETTER Z WITH ACUTE › LATIN SMALL LETTER Z
                case '\u017C':
                    return '\u007A'; // ż › z LATIN SMALL LETTER Z WITH DOT ABOVE › LATIN SMALL LETTER Z
                case '\u017E':
                    return '\u007A'; // ž › z LATIN SMALL LETTER Z WITH CARON › LATIN SMALL LETTER Z
                case '\u01B6':
                    return '\u007A'; // z › z LATIN SMALL LETTER Z WITH STROKE › LATIN SMALL LETTER Z
                case '\u0225':
                    return '\u007A'; // ? › z LATIN SMALL LETTER Z WITH HOOK › LATIN SMALL LETTER Z
                case '\u0290':
                    return '\u007A'; // ? › z LATIN SMALL LETTER Z WITH RETROFLEX HOOK › LATIN SMALL LETTER Z
                case '\u0291':
                    return '\u007A'; // ? › z LATIN SMALL LETTER Z WITH CURL › LATIN SMALL LETTER Z
                case '\u1E91':
                    return '\u007A'; // ? › z LATIN SMALL LETTER Z WITH CIRCUMFLEX › LATIN SMALL LETTER Z
                case '\u1E93':
                    return '\u007A'; // ? › z LATIN SMALL LETTER Z WITH DOT BELOW › LATIN SMALL LETTER Z
                case '\u1E95':
                    return '\u007A'; // ? › z LATIN SMALL LETTER Z WITH LINE BELOW › LATIN SMALL LETTER Z
                default:
                    return c;
            }
        }
    }
}