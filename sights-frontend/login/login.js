'use strict';
import Menu from '../components/Menu.js'
import Card from "../components/Card.js";
window.addEventListener("DOMContentLoaded", async () => {
    logIn();
});


function logIn(){
    const page = document.getElementById('page');
    const card = Card();
    const menu = Menu();  
    const login = document.createElement('p');
    login.innerHTML = 'Login';
    card.append(login)

    const inputUser = document.createElement("input");
    inputUser.setAttribute("type", "text");
    inputUser.placeholder ="Username"
    card.append(inputUser);

    const inputPassword = document.createElement("input");
    inputPassword.setAttribute("type", "text");
    inputPassword.placeholder ="Password"
    card.append(inputPassword);

    // const btnlogin = document.createElement('btnlogin');
    // btnlogin.innerText='Login'
    // card.append(btnlogin);
 

    // const btnsignup = document.createElement('btnsignup');
    // btnsignup.innerText='Sign Up'
    // card.append(btnsignup);
    
    
    page.append(card);
    page.append(menu);
    

}