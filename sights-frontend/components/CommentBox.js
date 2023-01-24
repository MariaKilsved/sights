export default function CommentBox(){
    const commentBox = document.createElement('div');
    commentBox.className = 'container';

    const commentBoxIMG = document.createElement('img');

    const commentText = document.createElement('p');
    commentText.innerHTML = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud'

    const commentedAt = document.createElement('span');
    commentedAt.className = 'time-right';
    commentedAt.innerHTML = '11.02';

    commentBoxIMG.src = '../Figma images/anonymous.png';
    commentBoxIMG.alt = 'Avatar';

    commentBox.appendChild(commentBoxIMG);
    commentBox.appendChild(commentText);
    commentBox.appendChild(commentedAt);

    return commentBox;
}