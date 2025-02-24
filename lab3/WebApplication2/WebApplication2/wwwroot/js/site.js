const messages = [
    "Ми всі у цьому разом... навіть коли нас чекає всесвітній конфлікт. ❤️",
    "Ми підемо разом на шляху до перемоги, не залежно від обставин! 💕",
    "Моя стріла точно потрапить в серце, навіть якщо воно механічне! 💘",
    "Ми можемо встояти разом, навіть проти Нульових! 💖",
    "Нехай твоя сила буде такою ж сильною, як і моя вистрілена стріла! 💝",
    "Ти — мій найкращий союзник, навіть якщо ми протистоїмо Ліззеру! 💗",
    "Не важливо, скільки разів нас відправляють у бій, я завжди буду поруч! 💕",
    "Я би пішов за тобою через всю галактику… навіть якщо це означає йти в обійми ворога! 💘",
    "З кожним моментом у твоїй компанії я стаю сильнішим. 💖",
    "Ми можемо вирішити наші конфлікти, як команда... разом ми сильніші! 💖"
];

let usedMessages = [];

function changeMessage() {
    if (usedMessages.length === messages.length) {
        usedMessages = [];
    }

    let randomMessage;
    do {
        randomMessage = messages[Math.floor(Math.random() * messages.length)];
    } while (usedMessages.includes(randomMessage));

    usedMessages.push(randomMessage);
    document.getElementById('message').innerText = randomMessage;
}

function createFloatingStickers() {
    for (let i = 1; i <= 18; i++) {
        const img = document.createElement('img');
        img.src = `/js/sticker${i}.webp`;
        img.classList.add('floating-image');
        img.style.left = `${Math.random() * 100}vw`;
        img.style.top = `${Math.random() * 100}vh`;
        document.body.appendChild(img);
    }
}

document.getElementById('message').addEventListener('click', changeMessage);

createFloatingStickers();