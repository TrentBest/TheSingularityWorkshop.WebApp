(() => {
    const supported = () => typeof window !== "undefined"
        && "speechSynthesis" in window
        && "SpeechSynthesisUtterance" in window;

    function stop() {
        if (!supported()) return false;
        window.speechSynthesis.cancel();
        return true;
    }

    function speak(text) {
        if (!supported() || !text) return false;
        window.speechSynthesis.cancel();
        const utterance = new SpeechSynthesisUtterance(text);
        utterance.lang = "en-US";
        utterance.rate = 0.96;
        utterance.pitch = 1.02;
        const voices = window.speechSynthesis.getVoices();
        const englishVoice = voices.find(voice => voice.lang && voice.lang.toLowerCase().startsWith("en"));
        if (englishVoice) utterance.voice = englishVoice;
        window.speechSynthesis.speak(utterance);
        return true;
    }

    window.workshopVoice = { speak, stop, supported: () => supported() };
})();
