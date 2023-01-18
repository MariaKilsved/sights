'use strict';
import Card from '../components/Card.js'

window.addEventListener("DOMContentLoaded", async () => {
    render();
});

function render(){
    const page = document.getElementById('page');
    const card = Card();
    page.append(card);
}