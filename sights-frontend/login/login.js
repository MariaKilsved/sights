'use strict';
import Card from "../components/Card.js";
import icon from '../components/logo.js';
import loginBtn from "../components/primaryButton.js";
import signupBtn from "../components/secondaryButton.js";
import {get} from '../lib/request.js'

window.addEventListener("DOMContentLoaded", async () => {
    logIn();

    const logbtn = document.getElementById('log-btn');

    logbtn.addEventListener('click', async () => {
        const username = document.getElementById('input-user').value;
        const password = document.getElementById('input-password').value;
        
        const response = await get(`https://localhost:7260/api/User/UserLogIn?username=${username}&password=${password}`);

        console.log(response);

        if(response == true){
            console.log('success');
        } else {
            console.log('failed');
        }
    });

});


function logIn(){
    const page = document.getElementById('page');
    const card = Card();
    
    const login = document.createElement('p');
    login.innerHTML = 'Login';
    card.append(login)

    const inputUser = document.createElement("input");
    inputUser.id = 'input-user';
    inputUser.setAttribute("type", "text");
    inputUser.placeholder ="Username"
    card.append(inputUser);

    const inputPassword = document.createElement("input");
    inputPassword.id = 'input-password';
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
    const signbtn = signupBtn('a');
    logbtn.innerHTML ='Login'
    logbtn.id = 'log-btn';
    signbtn.innerHTML ='Sign Up'
    signbtn.href = '/register'
    buttonContainer.append(logbtn);
    buttonContainer.append(signbtn);
    card.append(buttonContainer);
}