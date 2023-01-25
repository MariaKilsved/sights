export default function CommentBox(input){
    const commentBox = document.createElement('div');
    commentBox.className = 'container';

    const commentBoxIMG = document.createElement('img');

    const commentText = document.createElement('p');
    commentText.innerHTML = input;

    const commentedAt = document.createElement('span');
    commentedAt.className = 'time-left';

    const date = new Date().toLocaleString('en-GB', {year: "2-digit" , month: "2-digit", day: "2-digit"})
    const time = new Date().toLocaleString('en-gb', {hour: "2-digit", minute: "2-digit"});

    commentedAt.innerHTML =time + '<br/>' + date;

    commentBoxIMG.src = '../Figma images/anonymous.png';
    commentBoxIMG.alt = 'Avatar';

    commentBox.appendChild(commentBoxIMG);
    commentBox.appendChild(commentText);
    commentBox.appendChild(commentedAt);

    return commentBox;
}