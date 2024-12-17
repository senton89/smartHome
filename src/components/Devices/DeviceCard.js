import React from 'react';

const DeviceCard = ({ device, onEdit, onDelete }) => {
    return (
        <div className="bg-purple-50 p-4 rounded-lg shadow">
            <h3 className="text-purple-700 font-semibold">{device.name}</h3>
            <p className="text-purple-600">Status: {device.status}</p>
            <div className="flex justify-between mt-2">
                <button
                    className="text-purple-600 hover:text-purple-800"
                    onClick={() => onEdit(device)}
                >
                    Edit
                </button>
                <button
                    className="text-red-600 hover:text-red-800"
                    onClick={() => onDelete(device.id)}
                >
                    Delete
                </button>
            </div>
        </div>
    );
};

export default DeviceCard;