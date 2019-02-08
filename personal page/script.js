var navbar = document.getElementsByClassName("nav-item")

for(let i=0 ; i<6;i++){
    navbar[i].addEventListener("click",
    function (){
        for(let i=0 ; i<6;i++){
            navbar[i].classList.remove("active");
        }
        navbar[i].classList.add("active");
    }
    );
}


