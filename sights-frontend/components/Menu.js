import primaryButton from "./primaryButton.js";
import secondaryButton from "./secondaryButton.js";
import icon from "./logo.js";

export default function Menu(){
    const menu = document.createElement('div');
    menu.id = 'menu';

    const buttonContainer = document.createElement('div');
    buttonContainer.id = 'button-container';

    const logIn = primaryButton('a');
    logIn.innerHTML = 'login';
    logIn.href = '/login'
    
    const signup = secondaryButton('a');
    signup.innerHTML = 'signup';
    signup.href = '/register'

    const logo = icon();

    menu.append(logo);
    buttonContainer.append(logIn);
    buttonContainer.append(signup);
    menu.append(buttonContainer);
    return menu;
}