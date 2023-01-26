'use strict';

import Menu from "../components/Menu.js";
import Sight from "../components/Sight.js";
import CommentBox from '../components/CommentBox.js'
import AddCommentBox from "../components/AddCommentBox.js";
import { post, get } from '../lib/request.js';
import commentSamplePostData from './mock-data/MockData.js'

let upVoteNr = 0;
let downVoteNr = 0;

let canUpVote = true;
let canDownVote = true;

window.addEventListener("DOMContentLoaded", async () => {
    render();
    console.log(commentSamplePostData);
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
        const newComment = CommentBox(addCommentText.value);
        commentContainer.appendChild(newComment);
        commentSamplePostData.content = addCommentText.value;

        const storedUser = JSON.parse(localStorage.getItem('userinfo'));
        const queryString = window.location.search;
        const attractionId = new URLSearchParams(queryString).get('id');

        const comment = {
                userId: storedUser.userId,
                content: addCommentText.value,
                attractionId: attractionId,
        }   

         await post(`https://localhost:7260/api/comment`, comment);

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