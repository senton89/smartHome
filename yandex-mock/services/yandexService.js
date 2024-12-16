class YandexService {
    increaseVolume() {
        // Имитация увеличения громкости
        return 'Volume increased';
    }
    disableStation() {
        // Имитация отключения станции
        return 'Station disabled';
    }
    sayHello() {
        // Имитация команды 'сказать привет'
        return 'Hello from Alice';
    }
}

module.exports = new YandexService();