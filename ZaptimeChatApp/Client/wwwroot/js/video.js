// wwwroot/js/video.js

let session;
let publisher;
let subscriber;

async function initializeSession(apiKey, sessionId, token) {
    session = OT.initSession(apiKey, sessionId);

    // Create a publisher
    publisher = OT.initPublisher('publisher', {
        insertMode: 'append',
        width: '100%',
        height: '100%'
    });

    // Connect to the session
    session.connect(token, function (error) {
        if (error) {
            console.error('Failed to connect to session:', error.message);
        } else {
            // Publish the stream to the session
            session.publish(publisher);
        }
    });

    // Subscribe to a newly created stream
    session.on('streamCreated', function (event) {
        subscriber = session.subscribe(event.stream, 'subscriber', {
            insertMode: 'append',
            width: '100%',
            height: '100%'
        });
    });
}
