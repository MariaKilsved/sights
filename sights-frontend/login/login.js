'use strict';
import Card from "../components/Card.js";
import icon from '../components/logo.js';
import loginBtn from "../components/primaryButton.js";
import signupBtn from "../components/secondaryButton.js";
import {get} from '../lib/request.js';
    

window.addEventListener("DOMContentLoaded", async () => {
    render();

    const logbtn = document.getElementById('log-btn');

    logbtn.addEventListener('click', async () => {
        const username = document.getElementById('input-user').value;
        const password = document.getElementById('input-password').value;
        
        try {

            const response = await get(`https://localhost:7260/api/User/Login?username=${username}&password=${password}`, null);
            
            if (response.encryptedToken){
                localStorage.setItem('userinfo', JSON.stringify(response))
                window.alert(`Welcome ${username}`)
                window.location.href='/'
            }
            else window.alert('Failed to login');
        }catch(err){
            console.log(err);
        } 
            

    });

});

async function render(){
    const page = document.getElementById('page');
    const card = Card();
    
    const login = document.createElement('p');
    login.id='log-in'
    login.innerHTML = 'Login';
    card.append(login)

    const inputUser = document.createElement("input");
    inputUser.id = 'input-user';
    inputUser.setAttribute("type", "text");
    inputUser.placeholder ="Username"
    card.append(inputUser);

    const inputPassword = document.createElement("input");
    inputPassword.id = 'input-password';
    inputPassword.setAttribute("type", "password");
    inputPassword.placeholder ="Password"
    card.append(inputPassword);
    const logo = icon(); 
    logo.className = 'logoClass' ;
    page.append(logo);
    page.append(card);

    const buttonContainer = document.createElement('div');
    buttonContainer.id = 'button-container';
    
    const logbtn = loginBtn('button');
    const signUpBtn = signupBtn('a');
    logbtn.innerHTML ='Login'
    logbtn.id = 'log-btn';
    signUpBtn.innerHTML ='Sign Up'
    signUpBtn.href = '/register'

    const bg = document.createElement('img');
    bg.id = 'bg';
    bg.src = '../icons/bg.svg';
 

    buttonContainer.append(logbtn);
    buttonContainer.append(signUpBtn);
    card.append(buttonContainer);
    page.append(bg);
}
