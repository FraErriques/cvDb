using System;
using System.Text;



namespace Entity.FormatConverters
{


    public static class CharConversion
    {



        /// <summary>
        /// simbolo=  codice=0
        ///simbolo=☺ codice=1
        ///simbolo=☻ codice=2
        ///simbolo=♥ codice=3
        ///simbolo=♦ codice=4
        ///simbolo=♣ codice=5
        ///simbolo=♠ codice=6
        ///simbolo= codice=7
        ///simbolo codice=8
        ///simbolo=         codice=9
        ///simbolo=
        /// codice=10
        ///simbolo=♂ codice=11
        ///simbolo=♀ codice=12
        /// codice=13
        ///simbolo=♫ codice=14
        ///simbolo=☼ codice=15
        ///simbolo=► codice=16
        ///simbolo=◄ codice=17
        ///simbolo=↕ codice=18
        ///simbolo=‼ codice=19
        ///simbolo=¶ codice=20
        ///simbolo=§ codice=21
        ///simbolo=▬ codice=22
        ///simbolo=↨ codice=23
        ///simbolo=↑ codice=24
        ///simbolo=↓ codice=25
        ///simbolo=→ codice=26
        ///simbolo=← codice=27
        ///simbolo=∟ codice=28
        ///simbolo=↔ codice=29
        ///simbolo=▲ codice=30
        ///simbolo=▼ codice=31
        ///simbolo=  codice=32
        ///simbolo=! codice=33
        ///simbolo=" codice=34
        ///simbolo=# codice=35
        ///simbolo=$ codice=36
        ///simbolo=% codice=37
        ///simbolo=& codice=38
        ///simbolo=' codice=39
        ///simbolo=( codice=40
        ///simbolo=) codice=41
        ///simbolo=* codice=42
        ///simbolo=+ codice=43
        ///simbolo=, codice=44
        ///simbolo=- codice=45
        ///simbolo=. codice=46
        ///simbolo=/ codice=47
        ///simbolo=0 codice=48
        ///simbolo=1 codice=49
        ///simbolo=2 codice=50
        ///simbolo=3 codice=51
        ///simbolo=4 codice=52
        ///simbolo=5 codice=53
        ///simbolo=6 codice=54
        ///simbolo=7 codice=55
        ///simbolo=8 codice=56
        ///simbolo=9 codice=57
        ///simbolo=: codice=58
        ///simbolo=; codice=59
        ///simbolo=< codice=60
        ///simbolo== codice=61
        ///simbolo=> codice=62
        ///simbolo=? codice=63
        ///simbolo=@ codice=64
        ///simbolo=A codice=65
        ///simbolo=B codice=66
        ///simbolo=C codice=67
        ///simbolo=D codice=68
        ///simbolo=E codice=69
        ///simbolo=F codice=70
        ///simbolo=G codice=71
        ///simbolo=H codice=72
        ///simbolo=I codice=73
        ///simbolo=J codice=74
        ///simbolo=K codice=75
        ///simbolo=L codice=76
        ///simbolo=M codice=77
        ///simbolo=N codice=78
        ///simbolo=O codice=79
        ///simbolo=P codice=80
        ///simbolo=Q codice=81
        ///simbolo=R codice=82
        ///simbolo=S codice=83
        ///simbolo=T codice=84
        ///simbolo=U codice=85
        ///simbolo=V codice=86
        ///simbolo=W codice=87
        ///simbolo=X codice=88
        ///simbolo=Y codice=89
        ///simbolo=Z codice=90
        ///simbolo=[ codice=91
        ///simbolo=\ codice=92
        ///simbolo=] codice=93
        ///simbolo=^ codice=94
        ///simbolo=_ codice=95
        ///simbolo=` codice=96
        ///simbolo=a codice=97
        ///simbolo=b codice=98
        ///simbolo=c codice=99
        ///simbolo=d codice=100
        ///simbolo=e codice=101
        ///simbolo=f codice=102
        ///simbolo=g codice=103
        ///simbolo=h codice=104
        ///simbolo=i codice=105
        ///simbolo=j codice=106
        ///simbolo=k codice=107
        ///simbolo=l codice=108
        ///simbolo=m codice=109
        ///simbolo=n codice=110
        ///simbolo=o codice=111
        ///simbolo=p codice=112
        ///simbolo=q codice=113
        ///simbolo=r codice=114
        ///simbolo=s codice=115
        ///simbolo=t codice=116
        ///simbolo=u codice=117
        ///simbolo=v codice=118
        ///simbolo=w codice=119
        ///simbolo=x codice=120
        ///simbolo=y codice=121
        ///simbolo=z codice=122
        ///simbolo={ codice=123
        ///simbolo=| codice=124
        ///simbolo=} codice=125
        ///simbolo=~ codice=126
        ///simbolo=⌂ codice=127
        ///simbolo=? codice=128
        ///simbolo=? codice=129
        ///simbolo=? codice=130
        ///simbolo=? codice=131
        ///simbolo=? codice=132
        ///simbolo=? codice=133
        ///simbolo=? codice=134
        ///simbolo=? codice=135
        ///simbolo=? codice=136
        ///simbolo=? codice=137
        ///simbolo=? codice=138
        ///simbolo=? codice=139
        ///simbolo=? codice=140
        ///simbolo=? codice=141
        ///simbolo=? codice=142
        ///simbolo=? codice=143
        ///simbolo=? codice=144
        ///simbolo=? codice=145
        ///simbolo=? codice=146
        ///simbolo=? codice=147
        ///simbolo=? codice=148
        ///simbolo=? codice=149
        ///simbolo=? codice=150
        ///simbolo=? codice=151
        ///simbolo=? codice=152
        ///simbolo=? codice=153
        ///simbolo=? codice=154
        ///simbolo=? codice=155
        ///simbolo=? codice=156
        ///simbolo=? codice=157
        ///simbolo=? codice=158
        ///simbolo=? codice=159
        ///simbolo=  codice=160
        ///simbolo=¡ codice=161
        ///simbolo=¢ codice=162
        ///simbolo=£ codice=163
        ///simbolo=¤ codice=164
        ///simbolo=¥ codice=165
        ///simbolo=¦ codice=166
        ///simbolo=§ codice=167
        ///simbolo=¨ codice=168
        ///simbolo=© codice=169
        ///simbolo=ª codice=170
        ///simbolo=« codice=171
        ///simbolo=¬ codice=172
        ///simbolo=­ codice=173
        ///simbolo=® codice=174
        ///simbolo=¯ codice=175
        ///simbolo=° codice=176
        ///simbolo=± codice=177
        ///simbolo=² codice=178
        ///simbolo=³ codice=179
        ///simbolo=´ codice=180
        ///simbolo=µ codice=181
        ///simbolo=¶ codice=182
        ///simbolo=· codice=183
        ///simbolo=¸ codice=184
        ///simbolo=¹ codice=185
        ///simbolo=º codice=186
        ///simbolo=» codice=187
        ///simbolo=¼ codice=188
        ///simbolo=½ codice=189
        ///simbolo=¾ codice=190
        ///simbolo=¿ codice=191
        ///simbolo=À codice=192
        ///simbolo=Á codice=193
        ///simbolo=Â codice=194
        ///simbolo=Ã codice=195
        ///simbolo=Ä codice=196
        ///simbolo=Å codice=197
        ///simbolo=Æ codice=198
        ///simbolo=Ç codice=199
        ///simbolo=È codice=200
        ///simbolo=É codice=201
        ///simbolo=Ê codice=202
        ///simbolo=Ë codice=203
        ///simbolo=Ì codice=204
        ///simbolo=Í codice=205
        ///simbolo=Î codice=206
        ///simbolo=Ï codice=207
        ///simbolo=Ð codice=208
        ///simbolo=Ñ codice=209
        ///simbolo=Ò codice=210
        ///simbolo=Ó codice=211
        ///simbolo=Ô codice=212
        ///simbolo=Õ codice=213
        ///simbolo=Ö codice=214
        ///simbolo=× codice=215
        ///simbolo=Ø codice=216
        ///simbolo=Ù codice=217
        ///simbolo=Ú codice=218
        ///simbolo=Û codice=219
        ///simbolo=Ü codice=220
        ///simbolo=Ý codice=221
        ///simbolo=Þ codice=222
        ///simbolo=ß codice=223
        ///simbolo=à codice=224
        ///simbolo=á codice=225
        ///simbolo=â codice=226
        ///simbolo=ã codice=227
        ///simbolo=ä codice=228
        ///simbolo=å codice=229
        ///simbolo=æ codice=230
        ///simbolo=ç codice=231
        ///simbolo=è codice=232
        ///simbolo=é codice=233
        ///simbolo=ê codice=234
        ///simbolo=ë codice=235
        ///simbolo=ì codice=236
        ///simbolo=í codice=237
        ///simbolo=î codice=238
        ///simbolo=ï codice=239
        ///simbolo=ð codice=240
        ///simbolo=ñ codice=241
        ///simbolo=ò codice=242
        ///simbolo=ó codice=243
        ///simbolo=ô codice=244
        ///simbolo=õ codice=245
        ///simbolo=ö codice=246
        ///simbolo=÷ codice=247
        ///simbolo=ø codice=248
        ///simbolo=ù codice=249
        ///simbolo=ú codice=250
        ///simbolo=û codice=251
        ///simbolo=ü codice=252
        ///simbolo=ý codice=253
        ///simbolo=þ codice=254
        ///
        /// </summary>
        /// <param name="par"></param>
        /// <returns></returns>
        public static string substituteStrangeLetters(string par)
        {
            // non-letter symbols
            par = par.Replace('"', ' ');
            par = par.Replace('\'', ' ');
            par = par.Replace('.', ' ');
            par = par.Replace(',', ' ');
            par = par.Replace(';', ' ');
            par = par.Replace(':', ' ');
            // A group
            par = par.Replace('à', 'a');
            par = par.Replace('á', 'a');
            par = par.Replace('â', 'a');
            par = par.Replace('ã', 'a');
            par = par.Replace('ä', 'a');
            par = par.Replace('å', 'a');
            par = par.Replace('æ', 'a');
            par = par.Replace('À', 'A');
            par = par.Replace('Á', 'A');
            par = par.Replace('Â', 'A');
            par = par.Replace('Ã', 'A');
            par = par.Replace('Ä', 'A');
            par = par.Replace('Å', 'A');
            par = par.Replace('Æ', 'A');
            // E group
            par = par.Replace('è', 'e');
            par = par.Replace('é', 'e');
            par = par.Replace('ê', 'e');
            par = par.Replace('ë', 'e');
            par = par.Replace('È', 'E');
            par = par.Replace('É', 'E');
            par = par.Replace('Ê', 'E');
            par = par.Replace('Ë', 'E');
            // I group
            par = par.Replace('ì', 'i');
            par = par.Replace('í', 'i');
            par = par.Replace('î', 'i');
            par = par.Replace('ï', 'i');
            par = par.Replace('Ì', 'I');
            par = par.Replace('Í', 'I');
            par = par.Replace('Î', 'I');
            par = par.Replace('Ï', 'I');
            // O group
            par = par.Replace('ò', 'o');
            par = par.Replace('ó', 'o');
            par = par.Replace('ô', 'o');
            par = par.Replace('õ', 'o');
            par = par.Replace('ö', 'o');
            par = par.Replace('Ò', 'O');
            par = par.Replace('Ó', 'O');
            par = par.Replace('Ô', 'O');
            par = par.Replace('Õ', 'O');
            par = par.Replace('Ö', 'O');
            // U group
            par = par.Replace('ù', 'u');
            par = par.Replace('ú', 'u');
            par = par.Replace('û', 'u');
            par = par.Replace('ü', 'u');
            par = par.Replace('Ù', 'U');
            par = par.Replace('Ú', 'U');
            par = par.Replace('Û', 'U');
            par = par.Replace('Ü', 'U');
            // ready
            return par;
        }// end substituteStrangeLetters




    }// end class


}// end nmsp
