'use strict';

import Menu from "../components/Menu.js";
import Sight from "../components/Sight.js";
import CommentBox from '../components/CommentBox.js'
import AddCommentBox from "../components/AddCommentBox.js";

let upVoteNr = 0;
let downVoteNr = 0;

let canUpVote = true;
let canDownVote = true;

window.addEventListener("DOMContentLoaded", async () => {
    render();

    const upVote = document.getElementById('likesUpIMG');
    upVote.addEventListener('click', async () => {
        let upVote = document.getElementById('likesUp');

        if (canUpVote){
            upVoteNr++;
            upVote.textContent = upVoteNr;
            canUpVote = false;
        }
        else {
            upVoteNr--;
            upVote.textContent = upVoteNr;
            canUpVote = true;
        }
    })

    const downVote = document.getElementById('likesDownIMG');
    downVote.addEventListener('click', async () => {
        let downVote = document.getElementById('likesDown');

        if (canDownVote){
            downVoteNr++;
            downVote.textContent = downVoteNr;
            canDownVote = false;
        }
        else {
            downVoteNr--;
            downVote.textContent = downVoteNr;
            canDownVote = true;
        }
    })

    const sendMessageBtn = document.getElementById('sendMessageBtn');
    sendMessageBtn.addEventListener('click', async () => {
        const commentContainer = document.getElementById('commentContainer');
        const addCommentText = document.getElementById('addCommentBox');
        //send addcommenttext.value as input to CommentBox function
        const newComment = CommentBox(addCommentText.value);
        commentContainer.appendChild(newComment);
        addCommentText.value = '';
    })
});

function render(){
    
    const page = document.getElementById('page');
    const menu = Menu();
    const sight = Sight();
    const addCommentBox = AddCommentBox();

    const commentContainer = document.createElement('div');
    commentContainer.id = 'commentContainer';

    const commentBox = CommentBox('placeholder');
    const com2 = CommentBox('placeholder');

    commentContainer.append(commentBox);
    commentContainer.append(com2);

    page.append(menu);
    page.append(sight);
    page.append(commentContainer);
    page.append(addCommentBox);
}