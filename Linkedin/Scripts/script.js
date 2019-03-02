var navbar = document.getElementsByClassName("nav-item")
var comments = document.getElementsByClassName("comments")
var commentbtn = document.getElementsByClassName("combtn")
var likebtns = document.getElementsByClassName("fa-thumbs-up")
//for(let i=0 ; i<7;i++){
//    navbar[i].addEventListener("click",
//    function (){
//        for(let i=0 ; i<7;i++){
//            navbar[i].classList.remove("active");
//        }
//        navbar[i].classList.add("active");
//    }
//    );
//}

function AddLineUnder(i) {
    for (let i = 0; i < 6; i++) {
        navbar[i].classList.remove("active");
    }
    navbar[i].classList.add("active");
}

function ToggleActive() {
    comments[0].classList.toggle("hiddencom");
}

for (let i = 0; i < commentbtn.length; i++) {

    commentbtn[i].addEventListener("click", function () {
        console.log(i);
        var panel = comments[i];
        if (panel.style.maxHeight) {
            panel.style.maxHeight = null;
        }
        else {
            panel.style.maxHeight = panel.scrollHeight + "px";
        }

    });
}

function ToggleLike(i) {
    likebtns[i].classList.toggle("liked");
}
for (let i = 0; i < likebtns.length; i++) {
    likebtns[i].addEventListener("click",
        function () {
            ToggleLike(i)
        }
        );
}