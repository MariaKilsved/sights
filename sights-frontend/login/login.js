'use strict';
import Card from "../components/Card.js";
import icon from '../components/logo.js';
import loginBtn from "../components/primaryButton.js";
import signupBtn from "../components/secondaryButton.js";
window.addEventListener("DOMContentLoaded", async () => {
    logIn();
});


function logIn(){
   
    const page = document.getElementById('page');
    const card = Card();
    
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
    const logo = icon(); 
    logo.className = 'logoClass' ;
    page.append(logo);
    page.append(card);

   
    const buttonContainer = document.createElement('div');
    buttonContainer.id = 'button-container';
    const logbtn = loginBtn('button');
    const signbtn = signupBtn('button');
    logbtn.innerHTML ='Login'
    signbtn.innerHTML ='Sign Up'
    buttonContainer.append(logbtn);
    buttonContainer.append(signbtn);
    card.append(buttonContainer);
    
    

}