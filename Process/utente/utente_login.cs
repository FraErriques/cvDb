using System;
using System.Text;


namespace Process.utente
{

    /// <summary>
    /// Summary description for utente_login: (i.e. utente_loadSingle )
    ///		compares a couple{ web_username, web_pwd} with a db-row{ username, password, kkey}.
    /// </summary>
    public static class utente_login
    {
        /// 	0 ==  ok.
        ///		1 ==  no db connection.
        ///		2 ==  no such row; i.e. wrong usr.
        ///		3 ==  unexpected data irregularity.
        ///		4 ==  good username, wrong password.
        public static string[] loginMessages = null;

        /// <summary>
        /// static Ctor.
        /// 
        /// 	0 ==  ok.
        ///		1 ==  no db connection.
        ///		2 ==  no such row; i.e. wrong usr.
        ///		3 ==  unexpected data irregularity.
        ///		4 ==  good username, wrong password.
        /// </summary>
        static utente_login()
        {
            Process.utente.utente_login.loginMessages = new string[6];
            Process.utente.utente_login.loginMessages[0] = "Login Riuscita.";
            Process.utente.utente_login.loginMessages[1] = "No Db Connection.";
            Process.utente.utente_login.loginMessages[2] = "no such row; i.e. wrong usr.";
            Process.utente.utente_login.loginMessages[3] = "unexpected data irregularity.";
            Process.utente.utente_login.loginMessages[4] = "good username, wrong password.";
            Process.utente.utente_login.loginMessages[5] = "Sono stati fatti piu' di tre tentativi errati. E' necessario contattare l'Amministratore per essere riabilitati al servizio.";
        }// end static ctor




        /// <summary>
        /// NB. wrong usr is a blocking error. Nothing can be said
        ///		about the password, if the usr is wrong, since no
        ///		rows comes out from the query.
        /// </summary>
        /// <param name="web_username"></param>
        /// <param name="web_pwd"></param>
        /// <returns>
        ///		0 ==  ok.
        ///		1 ==  no db connection.
        ///		2 ==  no such row; i.e. wrong usr.
        ///		3 ==  unexpected data irregularity.
        ///		4 ==  good username, wrong password.
        /// </returns>
        public static int canLogOn(
            string web_username,	// username filled on the web-form, NB.filtered_________
            string web_pwd			// password filled on the web-form. NOT filtered________
            )
        {
            //------------- instantiate Entity  -----------------------------
            Entity.BusinessEntities.Utente utente =
                new Entity.BusinessEntities.Utente();
            utente.username = web_username;// unique
            //
            //------------- make use of instantiated Entity  -----------------------------
            int login_result =
                utente.canLogOn(
                    web_pwd, // crypted pwd, from the form.
                    null // trx
                );
            //----- result analysis----------
            if (4 == login_result)//--------------------------------------"good username, wrong password.";
            {// might be an intrusion attempt: write it down.
                bool user_row_in_lCrash =
                    Entity.Proxies.usp_lCrash_CheckLine_SERVICE.usp_lCrash_CheckLine(
                        utente.id
                    );
                if (!user_row_in_lCrash)
                {// stiil without a personal line in lCrash: let's write it down.
                    Entity.Proxies.usp_lCrash_INSERT_SERVICE.usp_lCrash_INSERT(
                        utente.id,
                        1 // first mistake
                    );
                }// end login wrong and  no user_row_in_lCrash.
                else//---"good username, wrong password." and user already has his row in lCrash.
                {
                    System.Data.DataTable current_crash_level =
                        Entity.Proxies.usp_lCrash_LOADSINGLE_SERVICE.usp_lCrash_LOADSINGLE(
                            utente.id
                        );
                    int int_current_crash_level =
                        (Int32)(current_crash_level.Rows[0]["card"]);
                    int new_current_crash_level = ++int_current_crash_level;// increment and then write down.
                    Entity.Proxies.usp_lCrash_UPDATE_SERVICE.usp_lCrash_UPDATE(
                        utente.id,
                        new_current_crash_level // increment and then write down.
                    );
                    if (3 < new_current_crash_level)
                    {
                        login_result = 5;// notify the exceeding wrong attemps, if the case.
                    }// else just say "good username, wrong pwd", i.e. 4.
                }// end //---"good username, wrong password." and user already has his row in lCrash.
            }// end if(4==login_result)//"good username, wrong password.";
            else if (0 == login_result)// login_riuscita
            {
                bool user_row_in_lCrash =
                    Entity.Proxies.usp_lCrash_CheckLine_SERVICE.usp_lCrash_CheckLine(
                        utente.id
                    );
                if (!user_row_in_lCrash)
                {// stiil without a personal line in lCrash: let's write it down.
                    Entity.Proxies.usp_lCrash_INSERT_SERVICE.usp_lCrash_INSERT(
                        utente.id,
                        0 // no mistakes yet.
                    );
                }// end if (!user_row_in_lCrash)
                else// login ok and user already has his row in lCrash.
                {
                    System.Data.DataTable current_crash_level =
                        Entity.Proxies.usp_lCrash_LOADSINGLE_SERVICE.usp_lCrash_LOADSINGLE(
                            utente.id
                        );
                    int int_current_crash_level =
                        (Int32)(current_crash_level.Rows[0]["card"]);
                    if (3 < int_current_crash_level)
                    {
                        // nothing to update, since login was ok; but equally cannot enter since he's got more then 3 mistakes.
                        login_result = 5; // i.e. "Sono stati fatti piu' di tre tentativi errati. E' necessario contattare l'Amministratore per essere riabilitati al servizio.";
                    }
                    else//--valid login, after<=3 mistakes: it clears the mistakes count.
                    {
                        Entity.Proxies.usp_lCrash_UPDATE_SERVICE.usp_lCrash_UPDATE(
                            utente.id,
                            0 // a valid login resets previous mistakes, iff they were<=3.
                        );
                    }// end---//--valid login, after<=3 mistakes: it clears the mistakes count.
                }// end---// login ok and user already has his row in lCrash.
            }
            //
            // ready
            return login_result;// user interface interpretes the code and renders the message.
        }// end canLogOn()



        public static string filterUsername(string webUsername)
        {
            System.Text.StringBuilder tmp_res = new StringBuilder(webUsername.Length);
            char[] separators = new char[13] { ' ', '.', '#', '@', '_', '+', '-', '*', '/', '^', ',', ';', ':' };
            string[] splitted = webUsername.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            //
            for (int c = 0; c < splitted.Length; c++)
            {
                for (int currentChar = 0; currentChar < splitted[c].Length; currentChar++)
                {
                    string tmp = new string(splitted[c][currentChar], 1);
                    //
                    if (0 == currentChar)
                    {
                        tmp_res.Append(tmp.ToUpper());//NB. redundant; ms-Sql is uncasesensitive.
                    }
                    else
                    {
                        tmp_res.Append(tmp);//  senza ToUpper.
                    }
                }// end single token treatment.
                tmp_res.Append(" ");//standard separator.// NB. indispensable to replace separators with blank, or with '%' in the query.
            }// end all token treatment.
            // ready
            return tmp_res.ToString();
        }//


    }// end class


}// end nmsp
