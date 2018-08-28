
function Page_Load_OnClient( controlName)
{
	textBoxUsername = window.document.getElementById( controlName);
	if( null != textBoxUsername )
	{
	    textBoxUsername.focus();
	}// else do nothing. The dynamic Login-Form is absent from the current page.
}//





function RedirectFromClient()
{// the following instruction makes a submit, on the specified url.
 // the following versions are equivalent.
	// relative:
	//window.document.location.pathname = "/vdir_20070919/Notiziario.aspx";
	// absolute:
	window.document.location.href = 'http://localhost/vdir_20070919/Notiziario.aspx';
}//


function OpenPopup()
{// the following instruction opens a child browser-window.
	// window.open(); new Browser
	window.open( // Popup
		'Notiziario.aspx', //path,
		'NotiziarioPopup',	//idpopup,
		'width=500, height=400,scrollbars=yes,left=50,top=50'
	);
}//


// NB. non fa nulla se la popup e' gia' aperta.
function OpenPopupManageSingleMail()
{// the following instruction opens a child browser-window.
	// window.open(); new Browser
	window.open( // Popup
		'ManageSingleMail.aspx', //path,
		'ManageSingleMailPopup',	//idpopup,
		'width=550, height=550,scrollbars=yes,left=50,top=50'
	);
}//


function ClosePopupManageSingleMail()
{// the following instruction closes a child browser-window.
	// reload data for the father-window datagrid.
	//window.opener.document.location.reload();// TODO mettere in get parRefresh if needed
//window.opener.document.location += '?refresh=true';
	txtMustRefresh = window.opener.document.getElementById('txtMustRefresh');
	window.close();// close the popup, i.e. the child window.
	txtMustRefresh.value = 'Yes';
//window.opener.document.location.reload();
}//


function VerificaCampiLogin()
{
	txtUsername = window.document.getElementById( 'txtUsername');
	if( txtUsername.value.length < 4)
	{
	 	alert('username too short');
	 	return false;
	}
	txtPassword = window.document.getElementById( 'txtPassword');
	if( txtPassword.value.length < 4)
	{
	 	alert('password too short');
	 	return false;
	}
	return true;
}


function nonSonoTuttiSpazi( par)
{
	result = false;
	for( c=0; c<par.length; c++)
	{
		if( ' ' != par.charAt(c) )
		{
			result = true;
			break;
		}
	}
	return result;
}


function VerificaCampiIscrizione()
{	
	txtUsername = window.document.getElementById( 'txtUsername');
	if( txtUsername.value.length < 4 ||
		! nonSonoTuttiSpazi( txtUsername.value) )
	{
	 	alert('username too short');
	 	return;
	}
	txtPassword = window.document.getElementById( 'txtPassword');
	if( txtPassword.value.length < 4 ||
		! nonSonoTuttiSpazi( txtPassword.value) )
	{
	 	alert('password too short');
	 	return;
	}
	txtConfermaPassword = window.document.getElementById( 'txtConfermaPassword');
	if( txtConfermaPassword.value != txtPassword.value)
	{
	 	alert('the two password differ');
	 	return;
	}
	// else can do Postback
	// NB. la form non va acquisita
	FormIscrizioneImpiegato.submit();
}

function ChiusuraSessione()
{
	window.close();
}
