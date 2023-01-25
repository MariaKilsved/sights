export default function logo(){
    const logo = document.createElement('img');
    logo.id = 'logo';
    logo.src = '../icons/Logo.svg';
    const link = document.createElement('a');
    link.href='/';
    link.append(logo);

    return link
}