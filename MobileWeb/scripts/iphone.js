/*
$(document).ready(function () {
    loadPage();
});
function loadPage(url) {
    if (url == undefined) {
        $('#container').load('index.html #header ul', hijackLinks);
    } else {
        $('#container').load(url + ' #content', hijackLinks);
    }
}
function hijackLinks() {
    $('#container a').click(function (e) {
        e.preventDefault();
        loadPage(e.target.href);
    });
}

*/
var MessageLoad='<div id="progress"><p>&nbsp;</p><p>Loading...</p><p>&nbsp;</p></div>';
var MessageError='<div class="styleError">There has been an error while processing your page. Please try again.</div>';

$(document).ready(function () {
    TraversePage();
});

function TraversePage(){

    //console.log('in TraversePage');
   // window.scrollTo(0, 0); resizeTo(320, 480);

    /* for browser detection use
       var browser=navigator.userAgent.toLowerCase();  
        var users_browser = ((browser.indexOf('iPhone')!=-1);  
        if (users_browser)  
        {  
            document.location.href='www.yourdomain.com/iphone_index.html';  
        } 
    */
	//Process Login
	//$('#login form').submit(ProcessLogin);

    //Process Login
    $('#loginbutton').tap(function (e) { ProcessLogin(); });

	// Process Logout
	$('.button.logout').click(function (e) {ProcessLogout(e);}); 
	// this syntax is incorrect for a callback: $('.button.logout').click(ProcessLogout(e));
	
	// a click is used instead of tap because it gives an error with jQT.goTo(MoveToURL, 'slide') 'Not able to tap element' error.
	$('a[href="#AppointmentList"]').click(function (e) {
		//e.preventDefault();
		//console.log('AppointmentList clicked');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		var MoveToURL='#AppointmentList';
		ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill);
	}); 
	
	
	$('#searchbutton').tap(function(e) {
		var searchterm=$('#searchpatientbox').val();
		//console.log('searchterm is dd' + searchterm);
		var UrlForFetchingData='PatientList.aspx?searchterm='+searchterm; 
		var SectionToFill='#PatientListContents';
		ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
	});
		
	$('a[href="#PatientList"]').tap(function (e) {
		//e.preventDefault();
		//console.log('PatientList clicked');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#PatientListContents';
		var MoveToURL='#PatientList';
		ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill);
		$('#searchpatientbox').val('');
	});

	// a tap function is used instead of .live() for elements loaded by AJAX
	// here the tap does not give an error with jQT.goTo(MoveToURL, 'slide')
	$('a[href="#AppointmentDetails"]').tap(function(e) {
		//console.log('AppointmentDetails tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentDetailsContents';
		var MoveToURL='#AppointmentDetails';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	$('a[href="#PatientDetails"]').tap(function(e) {
		//console.log('PatientDetails tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#PatientDetailsContents';
		var MoveToURL='#PatientDetails';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	/*previous, today and next buttons*/
	$('#previous').tap(function(e) {
	    //console.log('Previous button tapped');
	    var UrlForFetchingData = this.attributes["linkattib"].value;
	    var SectionToFill='#AppointmentListContents';
	    ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
	});
	
	$('#today').tap(function(e) {
		//console.log('Today button tapped');
		/*
        var UrlForFetchingData = 'AppointmentList.aspx'; 
		var SectionToFill='#AppointmentListContents';
		ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
        */
		var MoveToURL = '#FilterPicker';
		jQT.goTo(MoveToURL, 'slide');
        //for demo only
		var DemoDateCookieY = parseInt(getCookie("DemoDateCookieY"));
		var DemoDateCookieM = parseInt(getCookie("DemoDateCookieM"));
		var DemoDateCookieD = parseInt(getCookie("DemoDateCookieD"));
		if (DemoDateCookieY != null && DemoDateCookieY != "" && !isNaN(DemoDateCookieY)) {
		    $('#datepicker').datepicker("setDate", new Date(DemoDateCookieY, DemoDateCookieM - 1, DemoDateCookieD));
		}
	});

	$('#next').tap(function(e) {
		//console.log('Next button tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
	});
	
	/*home, appt, patient buttons*/
	$('.appts').tap(function(e) {
		//console.log('Next button tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		var MoveToURL='#AppointmentList';
		ProcessReversePageLink(UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	$('.patients').tap(function(e) {
		//console.log('patients button tapped');
		var searchterm=$('#searchpatientbox').val();
		//console.log('searchterm is ' + searchterm);
		var UrlForFetchingData='PatientList.aspx?searchterm='+searchterm; 
		var SectionToFill='#PatientListContents';
		var MoveToURL='#PatientList';
		ProcessReversePageLink(UrlForFetchingData, MoveToURL, SectionToFill);
    });

    /*Dennis: this overrides the default behavior of clicking the today button in jqueryui*/
    var _gotoToday = jQuery.datepicker._gotoToday;
    // datepicker is directly inside the jQuery object, so override that
    jQuery.datepicker._gotoToday = function (a) {
        var target = jQuery(a);
        var inst = this._getInst(target[0]);
        var DemoDateCookieY = parseInt(getCookie("DemoDateCookieY"));
        var DemoDateCookieM = parseInt(getCookie("DemoDateCookieM"));
        var DemoDateCookieD = parseInt(getCookie("DemoDateCookieD"));
        if (DemoDateCookieY != null && DemoDateCookieY != "" && !isNaN(DemoDateCookieY)) {
            inst.selectedYear = DemoDateCookieY;
            inst.selectedMonth = DemoDateCookieM - 1;
            inst.selectedDay = DemoDateCookieD;
        }
        // Dennis: if the default behaviour of the "Today" button is needed, uncomment next line
        // _gotoToday.call(this, a);
        // now do an additional call to _selectDate which will set the date and close
        // close the datepicker (if it is not inline)
        jQuery.datepicker._selectDate(a,
        jQuery.datepicker._formatDate(inst, inst.selectedDay, inst.selectedMonth, inst.selectedYear));
    }

    function getCookie(c_name) {
        var i, x, y, ARRcookies = document.cookie.split(";");
        for (i = 0; i < ARRcookies.length; i++) {
            x = ARRcookies[i].substr(0, ARRcookies[i].indexOf("="));
            y = ARRcookies[i].substr(ARRcookies[i].indexOf("=") + 1);
            x = x.replace(/^\s+|\s+$/g, "");
            if (x == c_name) {
                return unescape(y);
            }
        }
    }

    $("#datepicker").datepicker({
        onSelect: function (dateText, datePickerInstance) {
			var SelectedDate=$.datepicker.parseDate('mm/dd/yy', dateText);
			var d=SelectedDate.getDate();
			var m=SelectedDate.getMonth()+1; //getMonth() return 0 to 11
			var y=SelectedDate.getFullYear();
			var UrlForFetchingData = 'AppointmentList.aspx?year='+y+'&month='+m+'&day='+d; 
			var SectionToFill='#AppointmentListContents';
			var MoveToURL='#AppointmentList';
			ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill);
        },
        showButtonPanel: true
    });
	
	$('.home').click(function(e) { // tap event logs out the user on ipod.
		jQT.goToReverse('#home','slide');	
	});
	
	

}


function ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill){
	e.preventDefault();
	//console.log(' UrlForFetchingData =' + UrlForFetchingData );
	$(SectionToFill).append(MessageLoad);
	// for newly loaded links this is null
	if(e.currentTarget.attributes==null){
	//console.log('in this if statement');
	jQT.goTo(MoveToURL,'slide'); //do not use this line with tap event, it gives a 'Not able to tap element' error.
	}
	FetchPage(UrlForFetchingData, SectionToFill)
}

function ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill){
	//e.preventDefault();
	//console.log(' UrlForFetchingData =' + UrlForFetchingData );
    $(SectionToFill).append(MessageLoad);
	//no slide effect
	jQT.goTo(MoveToURL,''); //do not use this line with tap event, it gives a 'Not able to tap element' error.
	FetchPage(UrlForFetchingData, SectionToFill)
}

function ProcessReversePageLink(UrlForFetchingData, MoveToURL, SectionToFill){
    $(SectionToFill).append(MessageLoad);
	jQT.goToReverse(MoveToURL,'slide'); 
	FetchPage(UrlForFetchingData, SectionToFill)
}

function ProcessPreviousNextButton(e,UrlForFetchingData, SectionToFill){
	e.preventDefault();
	//console.log(' UrlForFetchingData =' + UrlForFetchingData );
	$(SectionToFill).append(MessageLoad);
	FetchPage(UrlForFetchingData, SectionToFill)
}
	
function FetchPage(UrlForFetchingData, SectionToFill){
	$.ajax({
		type: "GET",
		url: UrlForFetchingData,
		success: function (msg) {
			var $response = $(msg);
			var IsLoggedIn = $response.filter('#loggedin').text();//console.log(IsLoggedIn);
			var Content = $response.filter('#content').html();
			if(IsLoggedIn=='LoggedIn'){
				//console.log('still in session');
				$(SectionToFill).html(Content);
			}else{
            //console.log('session ended,about to flip');
             jQT.goTo('#login', 'flip');
			}
		},
        error: function(){
            $(SectionToFill).replaceWith(MessageError);// this takes care of a page not found or page not responding situation.
        }
	});

}	

function ProcessLogin() {
    var username = $('#username').val();
    var password = $('#password').val();
	var rememberusername = $('#rememberusername').attr('checked');
    var datatosent = "username=" + username + "&password=" + password+ "&rememberusername=" + rememberusername;
    //console.log(datatosent);
	//console.log('login clicked');
    $('#login').append(MessageLoad);
    $.ajax({
        type: "POST",
        url: "ProcessLogin.aspx",
        data: datatosent,
        success: function (msg) {
            $('#progress').replaceWith(''); //$('#login').remove('#progress') will not work
            if (msg == "CorrectLogin") {
                //console.log("here");
				$('#LabelMessage').text('');
                jQT.goTo('#home');
            } else {
                $('#LabelMessage').text(msg);
            }
        }
    });
    //jQT.goBack();
    return false;
}

function ProcessLogout(e) {
		//console.log('log out clicked');
		e.preventDefault();
		$('#home').append(MessageLoad);
		$.ajax({
			type: "GET",
			url: "ProcessLogout.aspx",
			data: "",
			success: function (msg) {
				if (msg == "LoggedOut") {
					$('#progress').replaceWith('');
					//console.log('in LoggedOut');
					jQT.goTo('#login');// no 'Not able to tap element' error.
				}
			}
		});

}

		

		
		
		
		