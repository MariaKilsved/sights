'use strict';
import Card from "../components/Card.js";
import icon from '../components/logo.js';
import registerBtn from "../components/primaryButton.js";
import cancelBtn from "../components/secondaryButton.js";
window.addEventListener("DOMContentLoaded", async () => {
    register();
});

function register(){
    const page = document.getElementById('page');
    const card = Card();
    
    const newAccount = document.createElement('p1');
    newAccount.innerHTML = 'Enter Account Information';
    card.append(newAccount)

    const inputAccountName= document.createElement("input");
    inputAccountName.setAttribute("type", "text");
    inputAccountName.placeholder ="Account name"
    card.append(inputAccountName);

    const inputFirstname = document.createElement("input");
    inputFirstname.setAttribute("type", "text");
    inputFirstname.placeholder ="Firstname"
    card.append(inputFirstname);

    const inputLastname = document.createElement("input");
    inputLastname.setAttribute("type", "text");
    inputLastname.placeholder ="Lastname"
    card.append(inputLastname);

    const inputEmail = document.createElement("input");
    inputEmail.setAttribute("type", "text");
    inputEmail.placeholder ="Email"
    card.append(inputEmail);

   
    const inputPasswordAccount = document.createElement("input");
    inputPasswordAccount.setAttribute("type", "text");
    inputPasswordAccount.placeholder ="Password"
    card.append(inputPasswordAccount);

    const inputConfirmPassword = document.createElement("input");
    inputConfirmPassword.setAttribute("type", "text");
    inputConfirmPassword.placeholder ="Confirm Password"
    card.append(inputConfirmPassword);
    page.append(card);
   
    
    const logo = icon(); 
    logo.className = 'logoClass' ;
    page.append(logo);

    const buttonContainer = document.createElement('div');
    buttonContainer.id = 'button-container';
    const regbtn = registerBtn('button');
    const canbtn = cancelBtn('button');
    regbtn.innerHTML ='Register'
    canbtn.innerHTML ='Cancel'
    buttonContainer.append(regbtn);
    buttonContainer.append(canbtn);
    card.append(buttonContainer);

}