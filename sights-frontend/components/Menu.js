import primaryButton from "./primaryButton.js";
import secondaryButton from "./secondaryButton.js";
import icon from "./logo.js";

export default function Menu(){

    const user = JSON.parse(window.localStorage.getItem('userinfo'));
    const menu = document.createElement('div');
    menu.id = 'menu';

    const buttonContainer = document.createElement('div');
    buttonContainer.id = 'button-container';

    let logIn;
    if(!user){
        logIn = primaryButton('a');
        logIn.innerHTML = 'Login';
        logIn.href = '/login'
    } else {
        logIn = primaryButton('a');
        logIn.innerHTML = 'Sign out';
        logIn.addEventListener('click', () => {
            window.alert('You are signed out')
            window.localStorage.clear();
            window.location.href='/'
        });
    }
    
    let signup;
    if(!user){
        signup = secondaryButton('a');
        signup.innerHTML = 'Sign up';
        signup.href = '/register'
    } else {
        signup = secondaryButton('a');
        signup.innerHTML = user.userName;
    }

    const logo = icon();

    menu.append(logo);
    buttonContainer.append(logIn);
    buttonContainer.append(signup);
    menu.append(buttonContainer);
    return menu;
}