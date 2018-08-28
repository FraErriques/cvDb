
	function setBrowserTitleBar( decoration)
	{
		d = new Date();
		//
		month = (d.getMonth()+1);// January==0
		if( month<=9)
			month = '0'+month;
		//else ok
		//	
		dayOfWeek = d.getDay();
		switch( dayOfWeek)
		{
			case 0:
			{
				dayOfWeekName = 'Sunday';
				break;
			}
			case 1:
			{
				dayOfWeekName = 'Monday';
				break;
			}
			case 2:
			{
				dayOfWeekName = 'Tuesday';
				break;
			}
			case 3:
			{
				dayOfWeekName = 'Wednesday';
				break;
			}
			case 4:
			{
				dayOfWeekName = 'Thursday';
				break;
			}
			case 5:
			{
				dayOfWeekName = 'Friday';
				break;
			}
			case 6:
			{
				dayOfWeekName = 'Saturday';
				break;
			}
		}
		//
		hours = d.getHours();
		if( hours<=9)
			hours = '0'+hours;
		//else ok		
		minutes = d.getMinutes();
		if( minutes<=9)
			minutes = '0'+minutes;
		//else ok
		seconds = d.getSeconds();
		if( seconds<=9)
			seconds = '0'+seconds;
		//else ok

		dayOfMonth = d.getDate();
		timestamp_date = '----/'+d.getFullYear()+'/'+month+'/'+dayOfWeekName+'_'+dayOfMonth+'/----';
		timestamp_time = '----/'+hours+':'+minutes+':'+seconds+'/----';
		window.document.title = '------------------' + timestamp_date + timestamp_time;
		if( null==decoration)
		{
		    window.document.title += '------------------------------------';
		}
		else
		{
		    window.document.title += '--'+decoration+'--';
		}		
	}
