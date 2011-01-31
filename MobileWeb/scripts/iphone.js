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

$(document).ready(function () {
    TraversePage();
});

function TraversePage(){

    //console.log('in TraversePage');
   // window.scrollTo(0, 0); resizeTo(320, 480);

	//Process Login
	$('#login form').submit(ProcessLogin);
	
	// Process Logout
	$('.button.logout').click(function (e) {ProcessLogout(e);}); 
	// this syntax is incorrect for a callback: $('.button.logout').click(ProcessLogout(e));
	
	// a click is used instead of tap because it gives an error with jQT.goTo(MoveToURL, 'slide') 'Not able to tap element' error.
	$('a[href="#AppointmentList"]').click(function (e) {
		//e.preventDefault();
		console.log('AppointmentList clicked');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		var MoveToURL='#AppointmentList';
		ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill);
	}); 
	
	
	$('#searchbutton').tap(function(e) {
		
		var searchterm=$('#searchpatientbox').val();
		console.log('searchterm is dd' + searchterm);
		var UrlForFetchingData='PatientList.aspx?searchterm='+searchterm; 
		var SectionToFill='#PatientListContents';
		ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
	});
		
	/*
	$('#searchpatientbox').keyup(function(e) {
		var searchterm=$('#searchpatientbox').val();
		console.log('searchterm is ' + searchterm);
		var UrlForFetchingData='PatientList.aspx?searchterm='+searchterm; 
		var SectionToFill='#PatientListContents';
		$(SectionToFill).append('<div id="progress">Loading...</div>');
		FetchPage(UrlForFetchingData, SectionToFill)

	});
*/
	$('a[href="#PatientList"]').tap(function (e) {
		//e.preventDefault();
		console.log('PatientList clicked');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#PatientListContents';
		var MoveToURL='#PatientList';
		ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill);
		$('#searchpatientbox').val('');
	}); 

	// a tap function is used instead of .live() for elements loaded by AJAX
	// here the tap does not give an error with jQT.goTo(MoveToURL, 'slide')
	$('a[href="#AppointmentDetails"]').tap(function(e) {
		console.log('AppointmentDetails tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentDetailsContents';
		var MoveToURL='#AppointmentDetails';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	$('a[href="#PatientDetails"]').tap(function(e) {
		console.log('PatientDetails tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#PatientDetailsContents';
		var MoveToURL='#PatientDetails';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	/*previous, today and next buttons*/
	$('.button.previous').tap(function(e) {
		console.log('Previous button tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
	});
	
	$('.button.today').tap(function(e) {
		console.log('Today button tapped');
		var UrlForFetchingData = 'AppointmentList.aspx'; 
		var SectionToFill='#AppointmentListContents';
		ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
	});
	
	
	$('.button.next').tap(function(e) {
		console.log('Next button tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		ProcessPreviousNextButton(e, UrlForFetchingData, SectionToFill);
	});
	
	/*home, appt, patient buttons*/
	$('.appts').tap(function(e) {
		console.log('Next button tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		var MoveToURL='#AppointmentList';
		ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	$('patients').tap(function(e) {
		console.log('patients button tapped');
		//var UrlForFetchingData = this.attributes["linkattib"].value; 
		var searchterm=$('#searchpatientbox').val();
		//console.log('searchterm is ' + searchterm);
		var UrlForFetchingData='PatientList.aspx?searchterm='+searchterm; 
		var SectionToFill='#PatientListContents';
		var MoveToURL='#PatientList';
		ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	$('.home').tap(function(e) {
	jQT.goTo('#home');
	});
	

}


function ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill){
	e.preventDefault();
	console.log(' UrlForFetchingData =' + UrlForFetchingData );
 	$(SectionToFill).append('<div id="progress">Loading...</div>');
	// for newly loaded links this is null
	if(e.currentTarget.attributes==null){
	console.log('in this if statement');
	jQT.goTo(MoveToURL,'slide'); //do not use this line with tap event, it gives a 'Not able to tap element' error.
	}
	FetchPage(UrlForFetchingData, SectionToFill)
}

function ProcessArrowlessPageLink(UrlForFetchingData, MoveToURL, SectionToFill){
	//e.preventDefault();
	console.log(' UrlForFetchingData =' + UrlForFetchingData );
 	$(SectionToFill).append('<div id="progress">Loading...</div>');
	//no slide effect
	jQT.goTo(MoveToURL,''); //do not use this line with tap event, it gives a 'Not able to tap element' error.
	FetchPage(UrlForFetchingData, SectionToFill)
}

function ProcessPreviousNextButton(e,UrlForFetchingData, SectionToFill){
	e.preventDefault();
	console.log(' UrlForFetchingData =' + UrlForFetchingData );
 	$(SectionToFill).append('<div id="progress">Loading...</div>');
	FetchPage(UrlForFetchingData, SectionToFill)
}
	
function FetchPage(UrlForFetchingData, SectionToFill){
	$.ajax({
		type: "GET",
		url: UrlForFetchingData,
		success: function (msg) {//console.log(msg);
			var $response = $(msg);
			var IsLoggedIn = $response.filter('#loggedin').text();
			var Content = $response.filter('#content').html();
			if(IsLoggedIn=='LoggedIn'){
				//console.log('still in session');
				$(SectionToFill).html(Content);
			}else{
				console.log('session ended');
				console.log('about to flip');
				jQT.goTo('#login', 'flip');
			}
		}
	});

}	
	

function ProcessLogin() {
    var username = $('#username').val();
    var password = $('#password').val();
	var rememberusername = $('#rememberusername').attr('checked');
    var datatosent = "username=" + username + "&password=" + password+ "&rememberusername=" + rememberusername;
//	console.log(datatosent);
	console.log('login clicked');
	$('#login').append('<div id="progress">Loading...</div>');
    $.ajax({
        type: "POST",
        url: "ProcessLogin.aspx",
        data: datatosent,
        success: function (msg) {
            //alert("---" + msg + "---");
            if (msg == "CorrectLogin") {
				$('#progress').replaceWith(''); //$('#login').remove('#progress') will not work
				//console.log("here");
                jQT.goTo('#home');

            } else {
                $('#LabelMessage').text(msg);
				$('#progress').replaceWith(''); 
            }
        }
    });
    //jQT.goBack();
    return false;
}


function ProcessLogout(e) {
		console.log('log out clicked');
		e.preventDefault();
		$('#home').append('<div id="progress">Loading...</div>');
		$.ajax({
			type: "GET",
			url: "ProcessLogout.aspx",
			data: "",
			success: function (msg) {
				if (msg == "LoggedOut") {
					$('#progress').replaceWith('');
					console.log('in LoggedOut');
					jQT.goTo('#login');// no 'Not able to tap element' error.
				}
			}
		});

}

