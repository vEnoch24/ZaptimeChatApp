
function playNotificationSound() {
    var audio = new Audio("~\\Client\\wwwroot\\js\\sounds\\notification.mp3"); // Path to sound file
    audio.play().catch(function (error) {
        console.log('Playback failed:', error);
    });
}