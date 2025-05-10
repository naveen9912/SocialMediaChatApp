let connection = new signalR.HubConnectionBuilder()
    .withUrl("/chathub")
    .build();

let dotnetHelper;

window.startChat = async (helper) => {
    dotnetHelper = helper;

    connection.on("ReceiveMessage", (user, message) => {
        dotnetHelper.invokeMethodAsync("ReceiveMessage", user, message);
    });

    try {
        await connection.start();
        console.log("SignalR connected.");
    } catch (err) {
        console.error("SignalR connection failed:", err);
    }
};

window.sendMessage = async (user, message) => {
    if (connection && connection.state === "Connected") {
        try {
            await connection.invoke("SendMessage", user, message);
        } catch (err) {
            console.error("Sending message failed:", err);
        }
    } else {
        console.warn("SignalR not connected.");
    }
};
