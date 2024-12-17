import axios from 'axios';

const API_URL = 'http://localhost:5202/api/rooms';

const createRoom = async (roomData) => {
    const response = await axios.post(API_URL, roomData);
    return response.data;
};

const deleteRoom = async (roomId) => {
    await axios.delete(`${API_URL}/${roomId}`);
};

const getRooms = async (filter) => {
    const response = await axios.get(API_URL, { params: filter });
    return response.data;
};

const getRoom = async (roomId) => {
    const response = await axios.get(`${API_URL}/${roomId}`);
    return response.data;
};

const updateRoom = async (roomId, roomData) => {
    await axios.patch(`${API_URL}/${roomId}`, roomData);
};

const connectDevices = async (roomId, devices) => {
    await axios.post(`${API_URL}/${roomId}/devices`, devices);
};

const disconnectDevices = async (roomId, devices) => {
    await axios.delete(`${API_URL}/${roomId}/devices`, { data: devices });
};

const findDevices = async (roomId, filter) => {
    const response = await axios.get(`${API_URL}/${roomId}/devices`, { params: filter });
    return response.data;
};

export {
    createRoom,
    deleteRoom,
    getRooms,
    getRoom,
    updateRoom,
    connectDevices,
    disconnectDevices,
    findDevices,
};