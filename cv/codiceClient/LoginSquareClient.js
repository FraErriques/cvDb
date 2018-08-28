// JScript File


function canLogOn()
{
    // acquire elements necessary within the script.
    txtUser = window.document.getElementById('LoginSquareClient1_txtUser');
    txtPwd = window.document.getElementById('LoginSquareClient1_txtPwd');
    //
    usrValidity = checkField( txtUser.value, 'username');// decides whether to show an alert or not.
    pwdValidity = checkField( txtPwd.value, 'password');// decides whether to show an alert or not.
    if( usrValidity && pwdValidity)
        return true;
    return false;
}// end function canLogOn()



function EnablePwdConfirmation()
{
    // acquire elements necessary within the script.
    txtUser = window.document.getElementById('txtUser');
    txtPwd = window.document.getElementById('txtPwd');
    lbl = window.document.getElementById('lblConfirmPwd');
    txtConfirm = window.document.getElementById('txtConfirmPwd');
    //
    if( null==txtConfirm.value     // pwd-confirmation field, not yet inserted.
        || ''==txtConfirm.value )
    {
        // stay on client
        lbl.disabled = false;
        txtConfirm.disabled = false;        
        txtConfirm.style.backgroundColor = 'yellow';// no reset needed, since it's going to server.
        return false;
    }
    else    // pwd-confirmation field filled.
    {
        usrValidity = checkField( txtUser.value, 'username');// decides whether to show an alert or not.
        pwdValidity = checkField( txtPwd.value, 'password');// decides whether to show an alert or not.
        if( 
            ! usrValidity
            || ! pwdValidity
            || txtPwd.value != txtConfirm.value   // pwd and confirmation DO NOT coincide.
            )
        {
            subscriptionResult = window.document.getElementById('lblEsitoIscrizione');
            subscriptionResult.visible = true;
            subscriptionResult.disabled = false;
            subscriptionResult.innerHTML = 'la pwd confermata deve coincidere con quella originale.';
            subscriptionResult.style.backgroundColor = 'yellow';// no reset needed, since it's going to server.
            return false;// do NOT postBack()
        }// END pwd and confirmation DO NOT coincide.
        // else can go server.
        // go server: the caller "returns" this script, which is the same of the following instruction.
        //frmMain.submit();
        return true;// postBack()
    }// end "else" pwd-confirmation field filled.
}//


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

function checkField(
    field,
    fieldName
    )
{
    currentCheckResult = false;
    //
    if( nonSonoTuttiSpazi( field)
        && field.length >= 4 )
        {
            // go on
            //  cannot yet return true; other checks have to be performed.
            currentCheckResult = true;
        }
        else
        {
            alert( fieldName + ' deve essere di almeno 4 caratteri, non tutti spazi.');
            currentCheckResult = false;
        }
    return currentCheckResult;
}//
