export default function CommentBox(input, username){
    const commentBox = document.createElement('div');
    commentBox.className = 'container';

    const commentBoxIMG = document.createElement('img');

    const userWhoCommented = document.createElement('p');
    userWhoCommented.className = 'user-who-commented-label';
    userWhoCommented.innerHTML = username;

    const commentText = document.createElement('p');
    commentText.innerHTML = input;

    commentBoxIMG.src = '../Figma images/anonymous.png';
    commentBoxIMG.alt = 'Avatar';

    commentBox.appendChild(commentBoxIMG);
    commentBox.appendChild(userWhoCommented);
    commentBox.appendChild(commentText);
  

    return commentBox;
}