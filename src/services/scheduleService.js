import axios from 'axios';

const API_URL = 'http://localhost:5202/api/schedules';

const createSchedule = async (scheduleData) => {
    const response = await axios.post(API_URL, scheduleData);
    return response.data;
};

const deleteSchedule = async (scheduleId) => {
    await axios.delete(`${API_URL}/${scheduleId}`);
};

const getSchedules = async (filter) => {
    const response = await axios.get(API_URL, { params: filter });
    return response.data;
};

const getSchedule = async (scheduleId) => {
    const response = await axios.get(`${API_URL}/${scheduleId}`);
    return response.data;
};

const updateSchedule = async (scheduleId, scheduleData) => {
    await axios.patch(`${API_URL}/${scheduleId}`, scheduleData);
};

const getDeviceForSchedule = async (scheduleId) => {
    const response = await axios.get(`${API_URL}/${scheduleId}/device`);
    return response.data;
};

export {
    createSchedule,
    deleteSchedule,
    getSchedules,
    getSchedule,
    updateSchedule,
    getDeviceForSchedule,
};