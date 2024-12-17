import React, { useState } from 'react';
import { createDevice, updateDevice } from '../../services/deviceService';

const DeviceForm = ({ device, onSuccess }) => {
    const [name, setName] = useState(device ? device.name : '');
    const [status, setStatus] = useState(device ? device.status : '');
    const [typeField, setTypeField] = useState(device ? device.typeField : '');

    const handleSubmit = async (e) => {
        e.preventDefault();
        const deviceData = { name, status, typeField };
        if (device) {
            await updateDevice(device.id, deviceData);
        } else {
            await createDevice(deviceData);
        }
        onSuccess();
    };

    return (
        <form onSubmit={handleSubmit}>
            <input
                type="text"
                value={name}
                onChange={(e) => setName(e.target.value)}
                placeholder="Device Name"
                required
            />
            <input
                type="text"
                value={status}
                onChange={(e) => setStatus(e.target.value)}
                placeholder="Status"
                required
            />
            <input
                type="text"
                value={typeField}
                onChange={(e) => setTypeField(e.target.value)}
                placeholder="Type"
                required
            />
            <button type="submit">{device ? 'Update' : 'Create'} Device</button>
        </form>
    );
};

export default DeviceForm;