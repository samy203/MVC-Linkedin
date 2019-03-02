var comments = document.getElementsByClassName("comments")
//////////////////////////////////////////////////////////////////////////////////////////
//Adding a like.
function addLike() {
    console.log('One Like');
}

//////////////////////////////////////////////////////////////////////////////////////////
//Subscribe an enter event for each comment.

 //Queries over the elements with the id starting with txtBox_
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
//////////////////////////////////////////////////////////////////////////////////////////
//Adding a comment.
function addComment(obj) {
    $.ajax({

        url: '/Feed/AddComment',

        data:
        {
            CommentContent: $('#txtBox_' + obj.CurrentPostID).val(),

            CurrentPostId: obj.CurrentPostID,

            ID: obj.ID
        },

        success: function (data) {
            var stringForSearch = '#commentz_' + obj.CurrentPostID;
            var result = $('<div />').append(data).find(stringForSearch).html();
            $(stringForSearch).html(result);
            var panel1 = document.querySelector('#commentz_' + obj.CurrentPostID);
            panel1.style.maxHeight = panel1.scrollHeight + 80 + "px";

        },

        type: 'POST'

    });
}
//////////////////////////////////////////////////////////////////////////////////////////
///Updates the friend div.
function redirectToAction(obj) {
    $.ajax({

        url: '/Feed/AddFriend',

        data: obj,

        success: function (data) {
            var result = $('<div />').append(data).find('#friendz').html();
            $('#friendz').html(result);
        },

        type: 'POST'

    });
}
///////////////////////////////////////////////////////////////////////////
