import axios from 'axios';

const API_URL = 'http://localhost:5202/api/yandexintegrations';

const disableStation = async (data) => {
    const response = await axios.post(`${API_URL}/disable-station`,{//, JSON.stringify(data), {
        headers: {
            'Content-Type': 'text/plain; charset=utf-8' // Если сервер ожидает текст
        }
    });
    return response.data;
};

const increaseVolume = async (data) => {
    const response = await axios.post(`${API_URL}/increase-volume`, data);
    return response.data;
};

const sayHello = async (data) => {
    const response = await axios.post(`${API_URL}/say-hello`, data);
    return response.data;
};

export {
    disableStation,
    increaseVolume,
    sayHello,
};