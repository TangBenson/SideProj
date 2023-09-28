import GoogleMapReact from 'google-map-react';

const AnyReactComponent = ({ text }) => <div>{text}</div>;

export default function Map(){
  const defaultProps = {
    center: {
      lat: 23.986132150477417,
      lng: 121.59077415058013
    },
    zoom: 11
  };

  return (
    // Important! Always set the container height explicitly
    <div style={{ height: '100vh', width: '100%' }}>
      <GoogleMapReact
        bootstrapURLKeys={{ key: "" }}
        defaultCenter={defaultProps.center}
        defaultZoom={defaultProps.zoom}
      >
        <AnyReactComponent
          lat={23.986132150477417}
          lng={121.59077415058013}
          text="My Marker"
        />
      </GoogleMapReact>
    </div>
  );
}