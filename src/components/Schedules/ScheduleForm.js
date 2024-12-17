import React, { useState, useEffect } from 'react';
import { createSchedule, updateSchedule } from '../../services/scheduleService';
import { getDevices } from '../../services/deviceService'; // Assuming you have a device service

const ScheduleForm = ({ schedule, onSuccess }) => {
    const [startTime, setStartTime] = useState(schedule ? schedule.startTime : '');
    const [endTime, setEndTime] = useState(schedule ? schedule.endTime : '');
    const [deviceId, setDeviceId] = useState(schedule ? schedule.device : '');
    const [devices, setDevices] = useState([]);

    useEffect(() => {
        const fetchDevices = async () => {
            const data = await getDevices({});
            setDevices(data);
        };
        fetchDevices();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const scheduleData = {startTime, endTime, device: {id: deviceId}};
        if (schedule) {
            await updateSchedule(schedule.id, scheduleData);
        } else {
            await createSchedule(scheduleData);
        }
        onSuccess();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="datetime-local"
                value={startTime}
                onChange={(e) => setStartTime(e.target.value)}
                required
            />
            <input
                type="datetime-local"
                value={endTime}
                onChange={(e) => setEndTime(e.target.value)}
                required
            />
            <select value={deviceId} onChange={(e) => setDeviceId(e.target.value)} required>
                <option value="">Select Device</option>
                {devices.map(device => (
                    <option key={device.id} value={device.id}>{device.name}</option>
                ))}
            </select>
            <button type="submit">{schedule ? 'Update' : 'Create'} Schedule</button>
        </form>
    );
};

export default ScheduleForm;