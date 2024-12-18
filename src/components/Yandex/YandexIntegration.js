import React from 'react';
import { disableStation, increaseVolume, sayHello } from '../../services/yandexIntegrationService';
import './YandexIntegration.css'; // Импортируем стили

const YandexIntegration = () => {
    const handleDisableStation = async () => {
        const answer = await disableStation("station data");
        alert(`station said: ${answer.message}`);
    };

    const handleIncreaseVolume = async () => {
        const answer = await increaseVolume("volume data");
        alert(`station said: ${answer.message}`);
    };

    const handleSayHello = async () => {
        const answer = await sayHello("hello data");
        alert(`station said: ${answer.message}`);
    };

    return (
        <div className="yandex-integration-container">
            <h2 className="yandex-integration-title">Yandex Integration</h2>
            <div className="yandex-integration-buttons">
                <button className="yandex-integration-button" onClick={handleDisableStation}>
                    Disable Station
                </button>
                <button className="yandex-integration-button" onClick={handleIncreaseVolume}>
                    Increase Volume
                </button>
                <button className="yandex-integration-button" onClick={handleSayHello}>
                    Say Hello
                </button>
            </div>
        </div>
    );
};

export default YandexIntegration;