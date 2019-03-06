//////////////////////////////////////////////////////////////////////////////////////////

    function setRefresher(obj) {
        setTimeout(refreshPost, 65000, obj)
    }
//////////////////////////////////////////////////////////////////////////////////////////
//////////////////////////////////////////////////////////////////////////////////////////

function refreshPost(obj) {

    console.log(obj.RecentDateString);

    $.ajax({

        url: '/Feed/RefreshPost',

        data:
        {
            ID: obj.ID,
            RecentDateString: obj.RecentDateString
        },

        cache: true,

        async: true,

        error: function () {

        },

        success: function (data) {

            obj.RecentDateString = getTimeNow();

            var result = $('<div />').append(data);

            $('#postref').prepend(result);

            var commentsAjax = document.getElementsByClassName("comments")

            var commentbtnAjax = document.getElementsByClassName("combtn");

            for (let i = 0; i < commentbtnAjax.length; i++) {

                commentbtnAjax[i].addEventListener("click", function () {
                    console.log(i);
                    var panel = commentsAjax[i];
                    if (panel.style.maxHeight) {
                        panel.style.maxHeight = null;
                    }
                    else {
                        panel.style.maxHeight = panel.scrollHeight + "px";
                    }

                });
            }



            let stringListAjax = [];

            $('*[id*=txtBox_]').each(function () {

                stringListAjax.push($(this).attr('id'));

                let x = $(this).attr('id').substring(7);

                $(this).keyup(function (event) {
                    if (event.keyCode === 13) {
                        $('#cmnt_' + x).click();
                    }
                });

            });
        },

        type: 'POST'

    });

    setTimeout(refreshPost, 65000, obj);

};

function getTimeNow() {
    let date = new Date();
    let day = date.getDate();       
    let month = date.getMonth() + 1;   
    let year = date.getFullYear();  
    let hour = date.getHours();     
    let minute = date.getMinutes(); 
    let second = date.getSeconds(); 
    "3/6/2019 1:58:17 PM"
    let ampm = hour >= 12 ? 'PM' : 'AM';
    hour = hour % 12;
    hour = hour ? hour : 12;
    minute = minute < 10 ? '0' + minute : minute;

    return time = month + "/" + day + "/" + year + " " + hour + ':' + minute + ':' + second+ " " +ampm;
}