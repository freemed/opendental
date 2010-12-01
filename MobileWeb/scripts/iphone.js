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
    
	/* menulevel 1 */
	//Process Login
	$('#login form').submit(ProcessLogin);
	
	// Process Logout
	$('.button.logout').click(function (e) {ProcessLogout(e);}); 
	// this syntax is incorrect for a callback: $('.button.logout').click(ProcessLogout(e));
	
	/* menulevel 1 ends here*/
	
	/* menulevel 2 */
	$('a[href="#AppointmentList"]').click(function (e) {
		console.log('AppointmentList clicked');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		var MoveToURL='#AppointmentList';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	}); 

	//Patients clicked.
	// a click is used instead of tap because it gives an error with jQT.goTo(MoveToURL, 'slide') 'Not able to tap element' error.
	$('a[href="#PatientList"]').click(function (e) {
		console.log('PatientList clicked');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#PatientListContents';
		var MoveToURL='#PatientList';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	}); 
	
		$('a[href="#AppointmentList"]').click(function (e) {
		console.log('AppointmentList clicked');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentListContents';
		var MoveToURL='#AppointmentList';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	}); 

	/* menulevel 2 ends here*/
	
	/* menulevel 3 */
	// a tap function is used instead of .live() for elements loaded by AJAX
	// here the tap does not give an error with jQT.goTo(MoveToURL, 'slide')
	$('a[href="#PatientDetails"]').tap(function(e) {
		console.log('PatientDetails tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#PatientDetailsContents';
		var MoveToURL='#PatientDetails';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	});
	
	
	
	$('a[href="#AppointmentDetails"]').tap(function(e) {
		console.log('AppointmentDetails tapped');
		var UrlForFetchingData = this.attributes["linkattib"].value; 
		var SectionToFill='#AppointmentDetailsContents';
		var MoveToURL='#AppointmentDetails';
		ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill);
	});
	

}


function ProcessNormalPageLink(e,UrlForFetchingData, MoveToURL, SectionToFill){
	e.preventDefault();
	console.log(' UrlForFetchingData =' + UrlForFetchingData );
 	$(SectionToFill).append('<div id="progress">Loading...</div>');
	
						// for newly loaded links this is null
						if(e.currentTarget.attributes==null){
						console.log('in this if statement');
						jQT.goTo(MoveToURL, 'slide'); //do not use this line with tap event, it gives a 'Not able to tap element' error.
							
						}
		
		$.ajax({
			type: "GET",
			url: UrlForFetchingData,
			success: function (msg) {
				var $response = $(msg);
				var IsLoggedIn = $response.filter('#loggedin').text();
				var Content = $response.filter('#content').html();
				if(IsLoggedIn=='LoggedIn'){

						$(SectionToFill).html(Content);
				}else{
				////console.log('about to flip');
				jQT.goTo('#login', 'flip');
				}
			}
		});
		
	}
	
function ProcessNormalPageLinkOld(e,targetsection){
	
	e.preventDefault();
    var urltarget = e.currentTarget.href;
	var UrlForFetchingData = e.currentTarget.attributes["linkattib"].value;

	////console.log('urltarget='+urltarget);
	////console.log('targetsection='+targetsection);
	console.log('UrlForFetchingData='+UrlForFetchingData);
		
	$(targetsection).append('<div id="progress">Loading...</div>');
	
		$.ajax({
			type: "GET",
			url: UrlForFetchingData,
			success: function (msg) {
				var $response = $(msg);
				var IsLoggedIn = $response.filter('#loggedin').text();
				var Content = $response.filter('#content').html();
				if(IsLoggedIn=='LoggedIn'){
				$(targetsection).html(Content);
				}else{
				////console.log('about to flip');
				jQT.goTo('#login', 'flip');
				}
			}
		});
		

}

function ProcessLogin() {
    var username = $('#username').val();
    var password = $('#password').val();
    var datatosent = "username=" + username + "&password=" + password;
	//console.log('username:' + username + ' password:' + password);
    $.ajax({
        type: "POST",
        url: "ProcessLogin.aspx",
        data: datatosent,
        success: function (msg) {
            //alert("---" + msg + "---");
            if (msg == "Correct Login") {
                //$('#login').remove(); 
                ////console.log("here");
                jQT.goTo('#home', 'flip');

            } else {
                $('#LabelMessage').text(msg);
            }
        }
    });
    //jQT.goBack();
    return false;
}


function ProcessLogout(e) {
		////console.log('log out clicked');
		e.preventDefault();
		$.ajax({
			type: "GET",
			url: "ProcessLogout.aspx",
			data: "",
			success: function (msg) {
				if (msg == "LoggedOut") {
				////console.log('in LoggedOut');
					jQT.goTo('#login', 'flip');// no 'Not able to tap element' error.
				}
			}
		});

}

