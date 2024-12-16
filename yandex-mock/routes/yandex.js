const express = require('express');
const router = express.Router();
const yandexService = require('../services/yandexService');

router.post('/increase-volume', (req, res) => {
    const result = yandexService.increaseVolume();
    res.status(200).json({ message: result });
});

router.post('/disable-station', (req, res) => {
    const result = yandexService.disableStation();
    res.status(200).json({ message: result });
});

router.post('/say-hello', (req, res) => {
    const result = yandexService.sayHello();
    res.status(200).json({ message: result });
});

module.exports = router;