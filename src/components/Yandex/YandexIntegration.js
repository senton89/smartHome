import React from 'react';
import { disableStation, increaseVolume, sayHello } from '../../services/yandexIntegrationService';

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
        <div className="bg-white p-4 rounded-lg shadow-md">
            <h2 className="text-2xl font-semibold text-purple-800">Yandex Integration</h2>
            <div className="mt-4 space-y-2">
                <button className="bg-purple-600 text-white p-2 rounded" onClick={handleDisableStation}>
                    Disable Station
                </button>
                <button className="bg-purple-600 text-white p-2 rounded" onClick={handleIncreaseVolume}>
                    Increase Volume
                </button>
                <button className="bg-purple-600 text-white p-2 rounded" onClick={handleSayHello}>
                    Say Hello
                </button>
            </div>
        </div>
    );
};

export default YandexIntegration;