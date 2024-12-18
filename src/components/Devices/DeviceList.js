import React, { useEffect, useState } from 'react';
import { getDevices, deleteDevice } from '../../services/deviceService';

const DeviceList = () => {
    const [devices, setDevices] = useState([]);

    const testDevices = [
        {
            id:"1",
            name:"device1",
            room:"1"
        },
        {
            id:"2",
            name:"device2",
            room:"1"
        },
        {
            id:"3",
            name:"device3",
            room:"1"
        }
    ]

    useEffect(() => {
        const fetchDevices = async () => {
            // const data = await getDevices({});
            // setDevices(data);
            setDevices(testDevices);
        };
        fetchDevices();
    }, []);

    const handleDelete = async (deviceId) => {
        await deleteDevice(deviceId);
        setDevices(devices.filter(device => device.id !== deviceId));
    };

    return (
        <div className="mt-4">
            <ul className="space-y-2">
                {devices.map((device) => (
                    <li key={device.id} className="bg-purple-50 p-2 rounded-lg shadow">
                        <div className="flex justify-between items-center">
                            <span className="text-purple-700">{device.name}</span>
                            <button
                                onClick={() => onEdit(device)}
                                className="text-purple-600 hover:text-purple-800"
                            >
                                Edit
                            </button>
                        </div>
                    </li>
                ))}
            </ul>
        </div>
    );
};

export default DeviceList;