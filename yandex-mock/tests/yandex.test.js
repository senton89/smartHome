const request = require('supertest');
const app = require('../app');
const yandexService = require('../services/yandexService');

// Мокируем функции yandexService
jest.mock('../services/yandexService', () => ({
    increaseVolume: jest.fn(() => 'Volume increased'),
    disableStation: jest.fn(() => 'Station disabled'),
    sayHello: jest.fn(() => 'Hello from Alice')
}));

describe('Yandex API', () => {
    test('POST /increase-volume should increase the volume', async () => {
        const response = await request(app)
            .post('/api/yandex/increase-volume')
            .send({ command: 'increase' });

        expect(response.status).toBe(200);
        expect(response.body.message).toBe('Volume increased');
    });

    test('POST /disable-station should disable the station', async () => {
        const response = await request(app)
            .post('/api/yandex/disable-station')
            .send({ command: 'disable' });

        expect(response.status).toBe(200);
        expect(response.body.message).toBe('Station disabled');
    });

    test('POST /say-hello should command Yandex Alice to say hello', async () => {
        const response = await request(app)
            .post('/api/yandex/say-hello')
            .send({ command: 'say_hello' });

        expect(response.status).toBe(200);
        expect(response.body.message).toBe('Hello from Alice');
    });
});