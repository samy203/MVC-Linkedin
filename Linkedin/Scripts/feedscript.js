
function addLike()
{
    console.log('One Like');

}

function addComment()
{
    console.log('one comment');
}






///Updates the friend div.
function redirectToAction(obj)
{
    $.ajax({

        url: '/Feed/AddFriend',

        data: obj,

        success: function (data)
        {
            var result = $('<div />').append(data).find('#friendz').html();
            $('#friendz').html(result);
        },

        type: 'POST'

    });
}
///////////////////////////////////////////////////////////////////////////
