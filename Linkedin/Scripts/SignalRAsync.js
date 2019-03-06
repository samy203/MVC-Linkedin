$.connection.hub.start()
    .done(function () { })
    .fail(function () { alert("Error") })


function signalrAnnounceLike(postID, op) {

    $.connection.linkedInHub.server.announceLike(postID, op);
}



////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////


//From client to server talking to it.
function signalrAnnounceComment(postID, op, commentWriterId ) {

    let targetbuttonId = 'txtBox_' + postID;

    let commentContent = document.getElementById(targetbuttonId).value;

    $.connection.linkedInHub.server.announceComment({ PostId: postID, PostUserId: op, CommentWriterId: commentWriterId, CommentContent: commentContent });
}


$.connection.linkedInHub.client.announceComment =
    function (postId, commentWriterId, commentContent) {
        let x = false;

        let domObj = $('#' + postId);

        $('.posts').each(function (i, obj) {
            if ($(this).attr('id') == postId) {
                x = true;
                domObj = obj;
            }
        });

        if (commentContent === "") {
            x = false;
        }

        if (x) {
            $.ajax({

                url: '/Feed/RefreshComment',

                data:
                {
                    CommentContent: commentContent,

                    ID: commentWriterId,

                    CurrentPostId: postId
                },

                success: function (data) {

                    console.log(data);

                    var stringForSearch = '#commentref_' + postId;

                    var togglerString = '#commentz_' + postId;

                    var result = $('<div />').append(data);

                    $(stringForSearch).replaceWith(result);

                    if ($(togglerString).css('max-height') == '0px') {

                    }
                    else {
                        var panel1 = document.querySelector('#commentz_' + postId);

                        panel1.style.maxHeight = panel1.scrollHeight + 80 + "px";
                    }


                    $('*[id*=txtBox_]').each(function () {

                        //Getting each full id of them then removing the portion of "txtBox_"
                        let x = $(this).attr('id').substring(7);


                        //Adding a function to the comment 
                        $(this).keyup(function (event) {
                            if (event.keyCode === 13) {
                                $('#cmnt_' + x).click();
                            }
                        });


                    });

                },

                type: 'POST'

            });

        }

        else {
        }
    }










